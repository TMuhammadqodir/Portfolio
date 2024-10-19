using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.IRepositories;
using Portfolio.Domain.Entities;
using Portfolio.Service.DTOs.ProjectAssets;
using Portfolio.Service.Exceptions;
using Portfolio.Service.Interfaces;

namespace Portfolio.Service.Services;

public class ProjectAssetService : IProjectAssetService
{
    private readonly IMapper mapper;
    private readonly IRepository<Asset> assetRepository;
    private readonly IRepository<Project> projectRepository;
    private readonly IRepository<ProjectAsset> projectAssetRepository;
    public ProjectAssetService(IMapper mapper,
                               IRepository<ProjectAsset> projectAssetRepository,
                               IRepository<Project> projectRepository,
                               IRepository<Asset> assetRepository)
    {
        this.mapper = mapper;
        this.assetRepository = assetRepository;
        this.projectRepository = projectRepository;
        this.projectAssetRepository = projectAssetRepository;
    }

    public async Task<ProjectAssetResultDto> CreateAsync(ProjectAssetCreationDto dto)
    {
        var existProject = await projectRepository.GetAsync(p => p.Id.Equals(dto.ProjectId))
            ?? throw new NotFoundException($"This project not found with id = {dto.ProjectId}");

        var existAsset = await assetRepository.GetAsync(a => a.Id.Equals(dto.AssetId))
            ?? throw new NotFoundException($"This asset not found with id = {dto.AssetId}");

        var mappedProjectAsset = this.mapper.Map<ProjectAsset>(dto);

        await this.projectAssetRepository.AddAsync(mappedProjectAsset);
        await this.projectAssetRepository.SaveAsync();

        return this.mapper.Map<ProjectAssetResultDto>(mappedProjectAsset);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existProjectAsset = await this.projectAssetRepository.GetAsync(e => e.Id.Equals(id))
            ?? throw new NotFoundException($"This projectAsset was not found with {id}");

        this.projectAssetRepository.Delete(existProjectAsset);
        await this.projectAssetRepository.SaveAsync();

        return true;
    }

    public async Task<ProjectAssetResultDto> GetByIdAsync(long id)
    {
        var existProjectAsset = await this.projectAssetRepository.GetAsync(e => e.Id.Equals(id))
            ?? throw new NotFoundException($"This projectAsset was not found with {id}");

        return this.mapper.Map<ProjectAssetResultDto>(existProjectAsset);
    }

    public async Task<IEnumerable<ProjectAssetResultDto>> GetAllAsync()
    {
        var projectAssets = await this.projectAssetRepository.GetAll()
            .ToListAsync();

        return this.mapper.Map<IEnumerable<ProjectAssetResultDto>>(projectAssets);
    }
}
