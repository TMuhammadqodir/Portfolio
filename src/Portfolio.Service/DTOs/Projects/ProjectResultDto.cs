using Portfolio.Domain.Entities;

namespace Portfolio.Service.DTOs.Projects;

public class ProjectResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public User User { get; set; }
    public ICollection<ProjectAsset> ProjectAssets { get; set; }
}
