﻿namespace Portfolio.Service.DTOs.Experiences;

public class ExperienceUpdateDto
{
    public string Title { get; set; }
    public string Company { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Address { get; set; }
    public bool IsCurrentJob { get; set; } = false;
}