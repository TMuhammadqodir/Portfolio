using Portfolio.Service.DTOs.Assets;

namespace Portfolio.Service.Interfaces;

public interface IAssetService
{
    Task<AssetResultDto> UploadImageAsync(AssetCreationDto dto, Enum type);
    Task<bool> DeleteImageAsync(long id);
}
