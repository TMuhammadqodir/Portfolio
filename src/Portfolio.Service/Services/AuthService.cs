﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Portfolio.DataAccess.IRepositories;
using Portfolio.Domain.Entities;
using Portfolio.Service.DTOs.Users;
using Portfolio.Service.Exceptions;
using Portfolio.Service.Helpers;
using Portfolio.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Portfolio.Service.Services;

public class AuthService : IAuthService
{
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly IRepository<User> userRepository;

    public AuthService(IMapper mapper,
        IConfiguration configuration,
        IRepository<User> repository)
    {
        this.mapper = mapper;
        this.userRepository = repository;
        this.configuration = configuration;
    }

    public async ValueTask<UserResponseDto> GenerateTokenAsync(string userName, string originalPassword)
    {
        var user = await this.userRepository.GetAsync(u => u.UserName.ToLower().Equals(userName.ToLower()))
            ?? throw new NotFoundException("This user is not found");

        bool verifiedPassword = PasswordHash.Verify(user.PasswordHash, originalPassword);
        if (!verifiedPassword)
            throw new CustomException(400, "Phone or password is invalid");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new Claim("UserName", user.UserName),
                 new Claim("Id", user.Id.ToString()),
                 new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var mapped = this.mapper.Map<UserResponseDto>(user);
        mapped.Token = tokenHandler.WriteToken(token);

        return mapped;
    }
}
