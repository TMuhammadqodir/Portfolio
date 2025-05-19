using Portfolio.Domain.Enums;

namespace Portfolio.Service.DTOs.Users;

public class UserUpdateDto
{
    public long? Id { get; set; }
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
}
