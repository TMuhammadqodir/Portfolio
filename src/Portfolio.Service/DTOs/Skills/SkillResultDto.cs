using Portfolio.Domain.Entities;

namespace Portfolio.Service.DTOs.Skills;

public class SkillResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public float Procentage { get; set; }
    public User User { get; set; }
}