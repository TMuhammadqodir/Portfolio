using Portfolio.Domain.Configurations;
using Portfolio.Domain.Enums;
using Portfolio.Service.DTOs.Assets;
using Portfolio.Service.DTOs.Projects;
using Portfolio.Service.DTOs.Users;

namespace Portfolio.Service.Interfaces;

public interface IUserService
{
    Task<UserResultDto> CreateAsync(UserCreationDto dto);
    Task<UserResultDto> UpdateAsync(UserUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<UserResultDto> GetByIdAsync(long id);
    Task<IEnumerable<UserResultDto>> GetAllAsync(PaginationParams @params, string search = null);
    Task<IEnumerable<UserResultDto>> GetAllAsync();
    Task<UserResultDto> UpgradeRoleAsync(long id, UserRole role);
    Task<UserResultDto> UploadImageOrVideoAsync(long id, AssetCreationDto dto, Enum type);
    Task<bool> DeleteImageOrVideoAsync(long id);
}
