using Microsoft.AspNetCore.Http;

namespace Portfolio.Service.DTOs.Assets;

public class AssetCreationDto
{
    public IFormFile FormFile { get; set; }
}
