using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Service.DTOs.Assets;

namespace Portfolio.Service.DTOs.UserAssets;

public class UserAssetResultDto
{
    public AssetResultDto Asset { get; set; }

    public UserUploadType UserUploadType { get; set; }
}
