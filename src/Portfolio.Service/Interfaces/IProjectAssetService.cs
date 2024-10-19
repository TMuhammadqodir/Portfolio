using Portfolio.Service.DTOs.ProjectAssets;

namespace Portfolio.Service.Interfaces;

public interface IProjectAssetService
{
    Task<ProjectAssetResultDto> CreateAsync(ProjectAssetCreationDto dto);
    Task<bool> DeleteAsync(long id);
    Task<ProjectAssetResultDto> GetByIdAsync(long id);
    Task<IEnumerable<ProjectAssetResultDto>> GetAllAsync();
}
