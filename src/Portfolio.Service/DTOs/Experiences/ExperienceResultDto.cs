using Portfolio.Domain.Entities;
using Portfolio.Service.DTOs.Users;

namespace Portfolio.Service.DTOs.Experiences;

public class ExperienceResultDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Company { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Address { get; set; }
    public bool IsCurrentJob { get; set; } = false;

    public UserResultDto User { get; set; }
}
