namespace Portfolio.Domain.Commons;

public class Auditable
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedtAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}
