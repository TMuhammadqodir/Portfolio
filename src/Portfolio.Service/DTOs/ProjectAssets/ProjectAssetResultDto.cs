using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Service.DTOs.Assets;

namespace Portfolio.Service.DTOs.ProjectAssets;

public class ProjectAssetResultDto
{
    public AssetResultDto Asset { get; set; }

    public ProjectUploadType ProjectUploadType { get; set; }
}