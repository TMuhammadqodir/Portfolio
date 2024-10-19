using Portfolio.Domain.Entities;
using Portfolio.Service.DTOs.Users;

namespace Portfolio.Service.DTOs.Skills;

public class SkillResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public float Procentage { get; set; }
    public UserResultDto User { get; set; }
}