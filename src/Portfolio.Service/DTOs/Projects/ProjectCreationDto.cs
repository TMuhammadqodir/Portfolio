namespace Portfolio.Service.DTOs.Projects;

public class ProjectCreationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? URL { get; set; }
    public long? UserId { get; set; }
}
