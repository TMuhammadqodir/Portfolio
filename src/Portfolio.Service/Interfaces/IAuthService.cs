using Portfolio.Domain.Enums;
using Portfolio.Service.DTOs.Users;

namespace Portfolio.Service.Interfaces;

public interface IAuthService
{
    ValueTask<UserResponseDto> GenerateTokenAsync(string userName, string originalPassword);
}
