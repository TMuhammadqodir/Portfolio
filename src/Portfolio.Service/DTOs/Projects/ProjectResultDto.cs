using Portfolio.Domain.Entities;
using Portfolio.Service.DTOs.ProjectAssets;
using Portfolio.Service.DTOs.Users;

namespace Portfolio.Service.DTOs.Projects;

public class ProjectResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? URL { get; set; }
    public UserResultDto User { get; set; }
    public ICollection<ProjectAssetResultDto> ProjectAssets { get; set; }
}
