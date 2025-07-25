﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Service.DTOs.Educations;
using Portfolio.Service.Helpers;
using Portfolio.Service.Interfaces;
using Portfolio.WebApi.Models;

namespace Portfolio.WebApi.Controllers;

public class EducationsController : BaseController
{
    private readonly IEducationService educationService;
    public EducationsController(IEducationService educationService)
    {
        this.educationService = educationService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(EducationCreationDto dto)
    {
        dto.UserId = HtppContextHelper.GetUserId();
    
        return Ok(new Response
           {
               StatusCode = 200,
               Message = "Succes",
               Data = await educationService.CreateAsync(dto)
           });
    }
    [HttpPost("update/{id:long}")]
    public async Task<IActionResult> UpdateAsync(long id, EducationUpdateDto dto)
    {
        dto.Id = id;

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await educationService.UpdateAsync(dto)
        });
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await educationService.DeleteAsync(id)
        });

    [AllowAnonymous]
    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await educationService.GetByIdAsync(id)
        });

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await educationService.GetAllAsync()
        });

    [HttpGet("get-by-user-id")]
    public async Task<IActionResult> GetByUserIdAsync(long userId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await educationService.GetByUserIdAsync(userId)
        });
}
