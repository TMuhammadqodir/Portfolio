using Portfolio.Domain.Commons;
using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Entities;

public class ProjectAsset : Auditable
{
    public long ProjectId { get; set; }
    public Project Project { get; set; }

    public long AssetId { get; set; }
    public Asset Asset { get; set; }

    public ProjectUploadType ProjectUploadType { get; set; }
}
