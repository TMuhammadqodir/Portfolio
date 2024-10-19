using Portfolio.Service.DTOs.UserAssets;

namespace Portfolio.Service.Interfaces;

public interface IUserAssetService
{
    Task<UserAssetResultDto> CreateAsync(UserAssetCreationDto dto);
    Task<bool> DeleteAsync(long id);
    Task<UserAssetResultDto> GetByIdAsync(long id);
    Task<IEnumerable<UserAssetResultDto>> GetAllAsync();
}
