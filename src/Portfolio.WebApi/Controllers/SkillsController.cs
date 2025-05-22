using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Service.DTOs.Skills;
using Portfolio.Service.Helpers;
using Portfolio.Service.Interfaces;
using Portfolio.Service.Services;
using Portfolio.WebApi.Models;

namespace Portfolio.WebApi.Controllers;

public class SkillsController : BaseController
{
    private readonly ISkillService skillService;
    public SkillsController(ISkillService skillService)
    {
        this.skillService = skillService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(SkillCreationDto dto)
    {
        dto.UserId = HtppContextHelper.GetUserId();

        return Ok(new Response
           {
               StatusCode = 200,
               Message = "Succes",
               Data = await skillService.CreateAsync(dto)
           });
    }

    [HttpPost("update/{id:long}")]
    public async Task<IActionResult> UpdateAsync(long id, SkillUpdateDto dto)
    {
        dto.Id = id;

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await skillService.UpdateAsync(dto)
        });
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await skillService.DeleteAsync(id)
        });

    [AllowAnonymous]
    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await skillService.GetByIdAsync(id)
        });

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await skillService.GetAllAsync()
        });

    [HttpGet("get-by-user-id")]
    public async Task<IActionResult> GetByUserIdAsync(long userId)
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await skillService.GetByUserIdAsync(userId)
       });
}
