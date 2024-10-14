using Portfolio.Domain.Commons;

namespace Portfolio.Domain.Entities;

public class Skill : Auditable
{
    public string Name { get; set; }
    public float Procentage { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }
}
