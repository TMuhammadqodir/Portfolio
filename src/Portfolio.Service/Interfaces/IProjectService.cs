using Portfolio.Service.DTOs.Assets;
using Portfolio.Service.DTOs.Projects;

namespace Portfolio.Service.Interfaces;

public interface IProjectService
{
    Task<ProjectResultDto> CreateAsync(ProjectCreationDto dto);
    Task<ProjectResultDto> UpdateAsync(ProjectUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<ProjectResultDto> GetByIdAsync(long id);
    Task<IEnumerable<ProjectResultDto>> GetAllAsync();
    Task<ProjectResultDto> UploadImageOrVideoAsync(long id, AssetCreationDto dto, Enum type);
    Task<ProjectResultDto> DeleteImageOrVideoAsync(long id);
}
