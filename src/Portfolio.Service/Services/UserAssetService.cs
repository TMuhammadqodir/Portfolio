using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.IRepositories;
using Portfolio.Domain.Entities;
using Portfolio.Service.DTOs.UserAssets;
using Portfolio.Service.Exceptions;
using Portfolio.Service.Interfaces;

namespace Portfolio.Service.Services;

public class UserAssetService : IUserAssetService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> userRepository;
    private readonly IRepository<Asset> assetRepository;
    private readonly IRepository<UserAsset> userAssetRepository;
    public UserAssetService(IMapper mapper,
                            IRepository<UserAsset> userAssetRepository,
                            IRepository<User> userRepository,
                            IRepository<Asset> assetRepository)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
        this.assetRepository = assetRepository;
        this.userAssetRepository = userAssetRepository;
    }

    public async Task<UserAssetResultDto> CreateAsync(UserAssetCreationDto dto)
    {
        var existUser = await this.userRepository.GetAsync(u => u.Id == dto.UserId)
            ?? throw new NotFoundException($"This user was not found with {dto.UserId}");

        var existAsset = await assetRepository.GetAsync(a => a.Id.Equals(dto.AssetId))
            ?? throw new NotFoundException($"This asset not found with id = {dto.AssetId}");

        var mappedUserAsset = this.mapper.Map<UserAsset>(dto);

        await this.userAssetRepository.AddAsync(mappedUserAsset);
        await this.userAssetRepository.SaveAsync();

        return this.mapper.Map<UserAssetResultDto>(mappedUserAsset);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existUserAsset = await this.userAssetRepository.GetAsync(e => e.Id.Equals(id))
            ?? throw new NotFoundException($"This userAsset was not found with {id}");

        this.userAssetRepository.Delete(existUserAsset);
        await this.userAssetRepository.SaveAsync();

        return true;
    }

    public async Task<UserAssetResultDto> GetByIdAsync(long id)
    {
        var existUserAsset = await this.userAssetRepository.GetAsync(e => e.Id.Equals(id))
            ?? throw new NotFoundException($"This userAsset was not found with {id}");

        return this.mapper.Map<UserAssetResultDto>(existUserAsset);
    }

    public async Task<IEnumerable<UserAssetResultDto>> GetAllAsync()
    {
        var userAssets = await this.userAssetRepository.GetAll()
            .ToListAsync();

        return this.mapper.Map<IEnumerable<UserAssetResultDto>>(userAssets);
    }
}
