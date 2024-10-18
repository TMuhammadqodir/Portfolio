using Portfolio.Domain.Enums;

namespace Portfolio.Service.DTOs.Educations;

public class EducationUpdateDto
{
    public long Id { get; set; }
    public string School { get; set; }
    public string FieldOfStudy { get; set; }
    public DegreeType Degree { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public float Grade { get; set; }
    public string Description { get; set; }
}
