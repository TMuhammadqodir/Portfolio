using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Service.DTOs.Educations;
using Portfolio.Service.DTOs.Experiences;
using Portfolio.Service.DTOs.Projects;
using Portfolio.Service.DTOs.Skills;
using Portfolio.Service.DTOs.UserAssets;

namespace Portfolio.Service.DTOs.Users;

public class UserResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string? Telegram { get; set; }
    public string? Github { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumbe { get; set; }
    public string? Info { get; set; }
    public UserRole Role { get; set; }

    public ICollection<EducationResultDto> Educations { get; set; }
    public ICollection<ExperienceResultDto> Experiences { get; set; }
    public ICollection<ProjectResultDto> Projects { get; set; }
    public ICollection<SkillResultDto> Skills { get; set; }
    public ICollection<UserAssetResultDto> UserAssets { get; set; }
}
