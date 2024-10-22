using Portfolio.Domain.Configurations;
using Portfolio.Domain.Enums;
using Portfolio.Service.DTOs.Assets;
using Portfolio.Service.DTOs.Projects;
using Portfolio.Service.DTOs.Users;

namespace Portfolio.Service.Interfaces;

public interface IUserService
{
    ValueTask<UserResultDto> AddAsync(UserCreationDto dto);
    ValueTask<UserResultDto> ModifyAsync(UserUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<UserResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<UserResultDto>> RetrieveAllAsync(PaginationParams @params, string search = null);
    ValueTask<IEnumerable<UserResultDto>> RetrieveAllAsync();
    ValueTask<UserResultDto> UpgradeRoleAsync(long id, UserRole role);
    Task<UserResultDto> UploadImageOrVideoAsync(long id, AssetCreationDto dto, Enum type);
    Task<bool> DeleteImageOrVideoAsync(long id);
}
