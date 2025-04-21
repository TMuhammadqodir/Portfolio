using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.IRepositories;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
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
    private readonly IRepository<ProjectAsset> projectAssetRepository;
    public ProjectService(IMapper mapper,
                            IRepository<Project> projectRepository,
                            IRepository<User> userRepository,
                            IAssetService assetService,
                            IProjectAssetService projectAssetService,
                            IRepository<ProjectAsset> projectAssetRepository)
    {
        this.mapper = mapper;
        this.projectRepository = projectRepository;
        this.userRepository = userRepository;
        this.assetService = assetService;
        this.projectAssetService = projectAssetService;
        this.projectAssetRepository = projectAssetRepository;
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
        var inclusion = new string[] { "ProjectAssets.Asset" };

        var existProject = await this.projectRepository.GetAsync(e => e.Id.Equals(id), inclusion)
            ?? throw new NotFoundException($"This project was not found with {id}");

        return this.mapper.Map<ProjectResultDto>(existProject);
    }

    public async Task<IEnumerable<ProjectResultDto>> GetAllAsync()
    {
        var inclusion = new string[] { "ProjectAssets.Asset" };

        var projects = await this.projectRepository.GetAll(includes: inclusion)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<ProjectResultDto>>(projects);
    }

    public async Task<ProjectResultDto> UploadImageOrVideoAsync(long id, AssetCreationDto dto, ProjectUploadType type)
    {
        var inclusion = new string[] { "ProjectAssets.Asset" };

        var existProject = await projectRepository.GetAsync(p => p.Id.Equals(id), inclusion)
            ?? throw new NotFoundException($"This project not found with id = {id}");

        var newAsset = await this.assetService.UploadImageAsync(dto, type);

        if (existProject.ProjectAssets.Any())
        {
            var projectAsset = existProject.ProjectAssets.First();

            if (type.ToString().Equals(projectAsset.ProjectUploadType.ToString()))
            {
                await this.DeleteImageOrVideoAsync(projectAsset.AssetId);
                existProject.ProjectAssets.Remove(projectAsset);
            }
            else if (existProject.ProjectAssets.Count > 1)
            {
                var projectAssetSecond = existProject.ProjectAssets.Skip(1).First();

                await this.DeleteImageOrVideoAsync(projectAssetSecond.AssetId);
                existProject.ProjectAssets.Remove(projectAssetSecond);
            }

        }

        var mappedProject = this.mapper.Map<ProjectResultDto>(existProject);

        var newProjectAsset = new ProjectAssetCreationDto()
        {
            ProjectId = id,
            AssetId = newAsset.Id,
            ProjectUploadType = type
        };

        mappedProject.ProjectAssets.Add(await this.projectAssetService.CreateAsync(newProjectAsset));

        return mappedProject;
    }
    public async Task<bool> DeleteImageOrVideoAsync(long id)
    {
        var inclusion = new string[] { "Asset" };

        var existUserAsset = await this.projectAssetRepository.GetAsync(pa => pa.Asset.Id.Equals(id), inclusion)
            ?? throw new NotFoundException($"asset was not foun with id = {id}");

        await this.projectAssetService.DeleteAsync(existUserAsset.Id);
        await this.assetService.DeleteImageAsync(id);

        return true;
    }
}
