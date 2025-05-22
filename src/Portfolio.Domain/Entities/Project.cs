using Portfolio.Domain.Commons;
using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Entities;

public class Project : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? URL { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }

    public ICollection<ProjectAsset> ProjectAssets { get; set; }
}
