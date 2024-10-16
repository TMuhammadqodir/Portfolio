﻿using Portfolio.Domain.Commons;
using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Entities;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }

    public UserRole Role { get; set; }

    public ICollection<Education> Educations { get; set; }
    public ICollection<Experience> Experiences { get; set; }
    public ICollection<Project> Projects { get; set; }
    public ICollection<Skill> Skills { get; set; }
    public ICollection<UserAsset> UserAssets { get; set; }
}
