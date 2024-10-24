﻿using Portfolio.Domain.Commons;

namespace Portfolio.Domain.Entities;

public class Experience : Auditable
{
    public string Title { get; set; }
    public string Company { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Address { get; set; }
    public bool IsCurrentJob { get; set; } = false;

    public long UserId { get; set; }
    public User User { get; set; }
}
