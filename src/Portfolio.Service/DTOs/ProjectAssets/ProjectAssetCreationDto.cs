using Portfolio.Domain.Enums;

namespace Portfolio.Service.DTOs.ProjectAssets;

public class ProjectAssetCreationDto
{
    public long ProjectId { get; set; }
    public long AssetId { get; set; }

    public ProjectUploadType ProjectUploadType { get; set; }
}
