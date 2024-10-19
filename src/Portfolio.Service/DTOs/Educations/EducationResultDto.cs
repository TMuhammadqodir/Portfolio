using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Service.DTOs.Users;

namespace Portfolio.Service.DTOs.Educations;

public class EducationResultDto
{
    public long Id { get; set; }
    public string School { get; set; }
    public string FieldOfStudy { get; set; }
    public DegreeType Degree { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public float Grade { get; set; }
    public string Description { get; set; }

    public UserResultDto User { get; set; }
}