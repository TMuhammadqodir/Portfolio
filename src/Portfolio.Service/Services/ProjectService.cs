using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.IRepositories;
using Portfolio.Domain.Entities;
using Portfolio.Service.DTOs.Assets;
using Portfolio.Service.DTOs.ProjectAssets;
using Portfolio.Service.DTOs.Projects;
using Portfolio.Service.Exceptions;
using Portfolio.Service.Interfaces;

namespace Portfolio.Service.Services;

public class ProjectService : IProjectService
{
    private readonly IMapper mapper;
    private readonly IRepository<Project> projectRepository;
    private readonly IRepository<User> userRepository;
    private readonly IAssetService assetService;
    private readonly IProjectAssetService projectAssetService;
    public ProjectService(IMapper mapper,
                            IRepository<Project> projectRepository,
                            IRepository<User> userRepository,
                            IAssetService assetService,
                            IProjectAssetService projectAssetService)
    {
        this.mapper = mapper;
        this.projectRepository = projectRepository;
        this.userRepository = userRepository;
        this.assetService = assetService;
        this.projectAssetService = projectAssetService;
    }

    public async Task<ProjectResultDto> CreateAsync(ProjectCreationDto dto)
    {
        var existUser = await this.userRepository.GetAsync(u => u.Id == dto.UserId)
            ?? throw new NotFoundException($"This user was not found with {dto.UserId}");

        var mappedProject = this.mapper.Map<Project>(dto);

        await this.projectRepository.AddAsync(mappedProject);
        await this.projectRepository.SaveAsync();

        return this.mapper.Map<ProjectResultDto>(mappedProject);
    }

    public async Task<ProjectResultDto> UpdateAsync(ProjectUpdateDto dto)
    {
        var existProject = await this.projectRepository.GetAsync(e => e.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This project was not found with {dto.Id}");

        var mappedProject = this.mapper.Map(dto, existProject);

        this.projectRepository.Update(mappedProject);
        await this.projectRepository.SaveAsync();

        return this.mapper.Map<ProjectResultDto>(mappedProject);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existProject = await this.projectRepository.GetAsync(e => e.Id.Equals(id))
            ?? throw new NotFoundException($"This project was not found with {id}");

        this.projectRepository.Delete(existProject);
        await this.projectRepository.SaveAsync();

        return true;
    }

    public async Task<ProjectResultDto> GetByIdAsync(long id)
    {
        var existProject = await this.projectRepository.GetAsync(e => e.Id.Equals(id))
            ?? throw new NotFoundException($"This project was not found with {id}");

        return this.mapper.Map<ProjectResultDto>(existProject);
    }

    public async Task<IEnumerable<ProjectResultDto>> GetAllAsync()
    {
        var projects = await this.projectRepository.GetAll()
            .ToListAsync();

        return this.mapper.Map<IEnumerable<ProjectResultDto>>(projects);
    }

    public async Task<ProjectResultDto> UploadImageOrVideoAsync(long id, AssetCreationDto dto, Enum type)
    {
        var inclusion = new string[] { "ProjectAsset.Asset" };

        var existProject = await projectRepository.GetAsync(p => p.Id.Equals(id), inclusion)
            ?? throw new NotFoundException($"This project not found with id = {id}");

        var newAsset = await this.assetService.UploadImageAsync(dto, type);

        if (existProject.ProjectAssets.Any())
        {
            var projectAsset = existProject.ProjectAssets.First();
            var projectAssetId = projectAsset.Id;

            if (type.ToString().Equals(projectAsset.ProjectUploadType.ToString()))
            {
                await this.assetService.DeleteImageAsync(projectAssetId);
                await this.projectAssetService.DeleteAsync(projectAssetId);
            }
            else if(existProject.ProjectAssets.Count > 1)
            {
                var projectAssetSecond = existProject.ProjectAssets.Skip(1).First();
                var projectAssetSecondId = projectAsset.Id;

                await this.assetService.DeleteImageAsync(projectAssetSecondId);
                await this.projectAssetService.DeleteAsync(projectAssetSecondId);
            }

        }

        var mappedProject = this.mapper.Map<ProjectResultDto>(existProject);

        var newProjectAsset = new ProjectAssetCreationDto()
        {
            ProjectId = id,
            AssetId = newAsset.Id
        };

        mappedProject.ProjectAssets.Add(await this.projectAssetService.CreateAsync(newProjectAsset));

        return mappedProject;
    }

    public Task<ProjectResultDto> DeleteImageOrVideoAsync(long id)
    {
        throw new NotImplementedException();
    }
}
