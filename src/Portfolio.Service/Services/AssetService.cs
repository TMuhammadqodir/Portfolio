using AutoMapper;
using Portfolio.DataAccess.IRepositories;
using Portfolio.Domain.Entities;
using Portfolio.Service.DTOs.Assets;
using Portfolio.Service.Exceptions;
using Portfolio.Service.Extensions;
using Portfolio.Service.Helpers;
using Portfolio.Service.Interfaces;
using System.Net.Mail;

namespace Portfolio.Service.Services;

public class AssetService : IAssetService
{
    private readonly IMapper mapper;
    private readonly IRepository<Asset> assetRepository;

    public AssetService(IMapper mapper, IRepository<Asset> assetRepository)
    {
        this.mapper = mapper;
        this.assetRepository = assetRepository;
    }

    public async Task<AssetResultDto> UploadImageAsync(AssetCreationDto dto)
    {
        var weebrootPath = Path.Combine(PathHelper.WebRootPath, "Images");

        if (!Directory.Exists(weebrootPath))
            Directory.CreateDirectory(weebrootPath);

        var fileExtention = Path.GetExtension(dto.FormFile.FileName);
        var fileName = $"{Guid.NewGuid().ToString("N")}{fileExtention}";
        var fullPath = Path.Combine(weebrootPath, fileName);

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        await fileStream.WriteAsync(dto.FormFile.ToByte());

        var createdAsset = new Asset
        {
            FileName = fileName,
            FilePath = fullPath,
        };

        await assetRepository.AddAsync(createdAsset);
        await assetRepository.SaveAsync();

        return mapper.Map<AssetResultDto>(createdAsset);
    }

    public async Task<bool> DeleteImageAsync(long id)
    {
        var existImage = await assetRepository.GetAsync(asset => asset.Id.Equals(id))
            ?? throw new NotFoundException($"This image was not found with {id}");

        assetRepository.Delete(existImage);
        await assetRepository.SaveAsync();

        return true;
    }
}
