using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;

namespace Portfolio.Service.DTOs.UserAssets;

public class UserAssetResultDto
{
    public Asset Asset { get; set; }

    public UserUploadType UserUploadType { get; set; }
}
