using Microsoft.AspNetCore.Http;

namespace Portfolio.Service.DTOs.Assets;

public class AssetsCreationDto
{
    public IFormFile FormFile { get; set; }
}
