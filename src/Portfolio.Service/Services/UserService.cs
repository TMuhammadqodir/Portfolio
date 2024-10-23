using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.IRepositories;
using Portfolio.Domain.Configurations;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Service.DTOs.Assets;
using Portfolio.Service.DTOs.UserAssets;
using Portfolio.Service.DTOs.Users;
using Portfolio.Service.Exceptions;
using Portfolio.Service.Extensions;
using Portfolio.Service.Helpers;
using Portfolio.Service.Interfaces;

namespace Portfolio.Service.Services;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> userRepository;
    private readonly IAssetService assetService;
    private readonly IUserAssetService userAssetService;
    private readonly IRepository<UserAsset>  userAssetRepository;

    public UserService(IMapper mapper,
                       IRepository<User> repository,
                       IAssetService assetService,
                       IUserAssetService userAssetService,
                       IRepository<UserAsset> userAssetRepository)
    {
        this.mapper = mapper;
        this.userRepository = repository;
        this.assetService = assetService;
        this.userAssetService = userAssetService;
        this.userAssetRepository = userAssetRepository;
    }

    public async Task<UserResultDto> CreateAsync(UserCreationDto dto)
    {
        var user = await this.userRepository.GetAsync(x =>
            x.UserName.ToLower().Equals(dto.UserName.ToLower()));
        if (user is not null)
            throw new AlreadyExistException($"This userName is already exist");

        var mappedUser = this.mapper.Map<User>(dto);

        mappedUser.PasswordHash = PasswordHash.Encrypt(dto.PasswordHash);
        await this.userRepository.AddAsync(mappedUser);
        await this.userRepository.SaveAsync();

        return this.mapper.Map<UserResultDto>(mappedUser);
    }

    public async Task<UserResultDto> UpdateAsync(UserUpdateDto dto)
    {
        var existUser = await this.userRepository.GetAsync(u => u.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This user is not found with Id = {dto.Id}");

        if (!existUser.UserName.Equals(dto.UserName, StringComparison.OrdinalIgnoreCase))
        {
            var userUserName = await this.userRepository.GetAsync(x =>
                x.UserName.ToLower().Equals(dto.UserName.ToLower()));

            if (userUserName is not null)
                throw new AlreadyExistException($"This userName already exist with id : {dto.UserName}");
        }

        this.mapper.Map(dto, existUser);

        existUser.PasswordHash = PasswordHash.Encrypt(dto.PasswordHash);
        this.userRepository.Update(existUser);
        await this.userRepository.SaveAsync();

        return this.mapper.Map<UserResultDto>(existUser);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existUser = await this.userRepository.GetAsync(u => u.Id.Equals(id), includes: new[] { "Asset" })
            ?? throw new NotFoundException($"This user is not found with Id = {id}");

        this.userRepository.Delete(existUser);
        await this.userRepository.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<UserResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var users = await this.userRepository.GetAll()
            .ToPaginate(@params)
            .ToListAsync();

        if (!string.IsNullOrEmpty(search))
            users = users.Where(user => user.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

        return this.mapper.Map<IEnumerable<UserResultDto>>(users);
    }

    public async Task<IEnumerable<UserResultDto>> GetAllAsync()
    {
        var users = await this.userRepository.GetAll().ToListAsync();
        return this.mapper.Map<IEnumerable<UserResultDto>>(users);
    }

    public async Task<UserResultDto> GetByIdAsync(long id)
    {
        var existUser = await this.userRepository.GetAsync(expression: u => u.Id.Equals(id))
            ?? throw new NotFoundException($"This user is not found with Id = {id}");

        return this.mapper.Map<UserResultDto>(existUser);
    }

    public async Task<UserResultDto> UpgradeRoleAsync(long id, UserRole role)
    {
        var existUser = await this.userRepository.GetAsync(u => u.Id.Equals(id))
            ?? throw new NotFoundException($"This user is not found with Id = {id}");

        existUser.Role = role;
        await this.userRepository.SaveAsync();

        return this.mapper.Map<UserResultDto>(existUser);
    }

    public async Task<UserResultDto> UploadImageOrVideoAsync(long id, AssetCreationDto dto, Enum type)
    {
        var inclusion = new string[] { "UserAsset.Asset" };

        var existUser = await userRepository.GetAsync(p => p.Id.Equals(id), inclusion)
            ?? throw new NotFoundException($"This user not found with id = {id}");

        var newAsset = await this.assetService.UploadImageAsync(dto, type);

        if (existUser.UserAssets.Any())
        {
            var userAsset = existUser.UserAssets.First();
            var userAssetId = userAsset.Id;

            if (type.ToString().Equals(userAsset.UserUploadType.ToString()))
            {
                await this.assetService.DeleteImageAsync(userAssetId);
                await this.userAssetService.DeleteAsync(userAssetId);
            }
            else if (existUser.UserAssets.Count > 1)
            {
                var userAssetSecond = existUser.UserAssets.Skip(1).First();
                var userAssetSecondId = userAssetSecond.Id;

                await this.assetService.DeleteImageAsync(userAssetSecondId);
                await this.userAssetService.DeleteAsync(userAssetSecondId);
            }

        }

        var mappedUser = this.mapper.Map<UserResultDto>(existUser);

        var newUserAsset = new UserAssetCreationDto()
        {
            UserId = id,
            AssetId = newAsset.Id
        };

        mappedUser.UserAssets.Add(await this.userAssetService.CreateAsync(newUserAsset));

        return mappedUser;
    }

    public async Task<bool> DeleteImageOrVideoAsync(long id)
    {
        var inclusion = new string[] { "Asset" };

        var existUserAsset = await this.userAssetRepository.GetAsync(pa => pa.Asset.Id.Equals(id), inclusion)
            ?? throw new NotFoundException($"asset was not foun with id = {id}");

        this.userAssetRepository.Delete(existUserAsset);
        await this.assetService.DeleteImageAsync(id);

        return true;
    }
}

