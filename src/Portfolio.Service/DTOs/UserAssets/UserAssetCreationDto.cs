using Portfolio.Domain.Enums;

namespace Portfolio.Service.DTOs.UserAssets;

public class UserAssetCreationDto
{
    public long UserId { get; set; }
    public long AssetId { get; set; }

    public UserUploadType UserUploadType { get; set; }
}
