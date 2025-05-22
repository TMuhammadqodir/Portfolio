using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Service.Interfaces;
using Portfolio.WebApi.Models;

namespace Portfolio.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ControllerForUser18 : ControllerBase
{
    private readonly IUserService userService;
    private readonly IEducationService educationService;
    private readonly IExperienceService experienceService;
    private readonly IProjectService projectService;
    private readonly ISkillService skillService;

    public ControllerForUser18(IUserService userService,
        IEducationService educationService,
        IExperienceService experienceService,
        IProjectService projectService,
        ISkillService skillService)
    {
        this.userService = userService;
        this.educationService = educationService;
        this.experienceService = experienceService;
        this.projectService = projectService;
        this.skillService = skillService;
    }

    [HttpGet("get-user18")]
    public async Task<IActionResult> GetByIdAsync()
      => Ok(new Response
      {
          StatusCode = 200,
          Message = "Success",
          Data = await this.userService.GetByIdAsync(18)
      });

    [HttpGet("get-education-by-user18")]
    public async Task<IActionResult> GetEducationByUserIdAsync()
    => Ok(new Response
    {
        StatusCode = 200,
        Message = "Success",
        Data = await educationService.GetByUserIdAsync(18)
    });

    [HttpGet("get-experience-by-user18")]
    public async Task<IActionResult> GetExperienceByUserIdAsync()
    => Ok(new Response
    {
        StatusCode = 200,
        Message = "Success",
        Data = await experienceService.GetByUserIdAsync(18)
    });

    [HttpGet("get-project-by-user18")]
    public async Task<IActionResult> GetProjectByUserIdAsync()
    => Ok(new Response
    {
        StatusCode = 200,
        Message = "Success",
        Data = await projectService.GetByUserIdAsync(18)
    });

    [HttpGet("get-skill-by-user18")]
    public async Task<IActionResult> GetSkillByUserIdAsync()
    => Ok(new Response
    {
        StatusCode = 200,
        Message = "Success",
        Data = await skillService.GetByUserIdAsync(18)
    });
}
