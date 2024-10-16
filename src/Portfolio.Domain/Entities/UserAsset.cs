using Portfolio.Domain.Commons;
using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Entities;

public class UserAsset : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long AssetId { get; set; }
    public Asset Asset { get; set; }

    public UserUploadType UserUploadType { get; set; }
}
