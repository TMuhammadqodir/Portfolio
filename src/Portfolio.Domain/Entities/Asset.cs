using Portfolio.Domain.Commons;

namespace Portfolio.Domain.Entities;

public class Asset : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }

    public ICollection<ProjectAsset> ProjectAssets { get; set; }
    public ICollection<UserAsset> UserAssets { get; set; }
}
