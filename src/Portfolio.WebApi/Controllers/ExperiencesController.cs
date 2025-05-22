using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Service.DTOs.Experiences;
using Portfolio.Service.Helpers;
using Portfolio.Service.Interfaces;
using Portfolio.WebApi.Models;

namespace Portfolio.WebApi.Controllers;

public class ExperiencesController : BaseController
{
    private readonly IExperienceService experienceService;
    public ExperiencesController(IExperienceService experienceService)
    {
        this.experienceService = experienceService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(ExperienceCreationDto dto)
    { 
        dto.UserId = HtppContextHelper.GetUserId();

        return Ok(new Response
           {
               StatusCode = 200,
               Message = "Succes",
               Data = await experienceService.CreateAsync(dto)
           });
    }

    [HttpPost("update/{id:long}")]
    public async Task<IActionResult> UpdateAsync(long id, ExperienceUpdateDto dto)
    {
        dto.Id = id;

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await experienceService.UpdateAsync(dto)
        });
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await experienceService.DeleteAsync(id)
        });

    [AllowAnonymous]
    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await experienceService.GetByIdAsync(id)
        });

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await experienceService.GetAllAsync()
        });

    [HttpGet("get-by-user-id")]
    public async Task<IActionResult> GetByUserIdAsync(long userId)
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await experienceService.GetByUserIdAsync(userId)
       });
}
