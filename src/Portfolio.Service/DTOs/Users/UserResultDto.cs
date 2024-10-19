using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Service.DTOs.Educations;
using Portfolio.Service.DTOs.Experiences;
using Portfolio.Service.DTOs.Projects;
using Portfolio.Service.DTOs.Skills;

namespace Portfolio.Service.DTOs.Users;

public class UserResultDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }

    public UserRole Role { get; set; }

    public ICollection<EducationResultDto> Educations { get; set; }
    public ICollection<ExperienceResultDto> Experiences { get; set; }
    public ICollection<ProjectResultDto> Projects { get; set; }
    public ICollection<SkillResultDto> Skills { get; set; }
    public ICollection<UserResultDto> UserAssets { get; set; }
}
