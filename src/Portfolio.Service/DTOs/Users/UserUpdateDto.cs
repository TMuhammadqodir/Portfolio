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

    public UserRole Role { get; set; }
}
