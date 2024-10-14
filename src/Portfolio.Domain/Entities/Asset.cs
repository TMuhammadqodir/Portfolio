using Portfolio.Domain.Commons;

namespace Portfolio.Domain.Entities;

public class Asset : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
}
