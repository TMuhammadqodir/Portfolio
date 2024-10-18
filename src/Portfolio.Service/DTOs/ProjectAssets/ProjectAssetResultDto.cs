using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;

namespace Portfolio.Service.DTOs.ProjectAssets;

public class ProjectAssetResultDto
{
    public Asset Asset { get; set; }

    public ProjectUploadType ProjectUploadType { get; set; }
}