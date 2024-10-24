﻿using Portfolio.Domain.Enums;
using Portfolio.Service.DTOs.Users;

namespace Portfolio.Service.Interfaces;

public interface IAuthService
{
    Task<UserResponseDto> GenerateTokenAsync(string userName, string originalPassword);
}
