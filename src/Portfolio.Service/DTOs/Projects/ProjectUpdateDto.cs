using Microsoft.AspNetCore.Http;

namespace Portfolio.Service.DTOs.Projects;

public class ProjectUpdateDto
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? URL { get; set; }
}
