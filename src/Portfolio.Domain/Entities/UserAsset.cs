using Portfolio.Domain.Commons;

namespace Portfolio.Domain.Entities;

public class UserAsset : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long AssetId { get; set; }
    public Asset Asset { get; set; }
}
