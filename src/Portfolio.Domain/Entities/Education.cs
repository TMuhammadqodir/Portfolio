using Portfolio.Domain.Commons;
using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Entities;

public class Education : Auditable
{
    public string School { get; set; }
    public string FieldOfStudy { get; set; }
    public DegreeType Degree { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public float Grade { get; set; }
    public string Description { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }
}
