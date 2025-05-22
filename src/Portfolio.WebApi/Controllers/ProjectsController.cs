using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Enums;
using Portfolio.Service.DTOs.Assets;
using Portfolio.Service.DTOs.Projects;
using Portfolio.Service.Helpers;
using Portfolio.Service.Interfaces;
using Portfolio.Service.Services;
using Portfolio.WebApi.Models;

namespace Portfolio.WebApi.Controllers;

public class ProjectsController : BaseController
{
    private readonly IProjectService projectService;
    public ProjectsController(IProjectService projectService)
    {
        this.projectService = projectService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromForm] ProjectCreationDto projectCreationDto,  ProjectUploadType type)
    { 
        var projectResult = await projectService.CreateAsync(projectCreationDto);

        var assetCreationDto = new AssetCreationDto()
        {
            FormFile = projectCreationDto.FormFile,
        };

        var projectResultDto = await UploadImageAsync(projectResult.Id, assetCreationDto, type);

        return Ok(new Response
           {
               StatusCode = 200,
               Message = "Succes",
               Data = projectResultDto
           });
    }

    [HttpPost("update/{id:long}")]
    public async Task<IActionResult> UpdateAsync(long id, [FromForm] ProjectUpdateDto projectUpdateDto, ProjectUploadType type)
    {
        projectUpdateDto.Id = id;

        var updateProject = await projectService.UpdateAsync(projectUpdateDto);

        var assetCreationDto = new AssetCreationDto()
        {
            FormFile = projectUpdateDto.FormFile,
        };

        var projectResultDto = await UploadImageAsync(updateProject.Id, assetCreationDto, type);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = projectResultDto
        });
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await projectService.DeleteAsync(id)
        });

    [AllowAnonymous]
    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await projectService.GetByIdAsync(id)
        });

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await projectService.GetAllAsync()
        });

    [HttpGet("get-by-user-id")]
    public async Task<IActionResult> GetByUserIdAsync(long userId)
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await projectService.GetByUserIdAsync(userId)
       });

    [HttpPost("image-upload")]
    public async Task<IActionResult> UploadImageAsync(long productId, [FromForm] AssetCreationDto dto, ProjectUploadType type)
    => Ok(new Response
    {
        StatusCode = 200,
        Message = "Success",
        Data = await this.projectService.UploadImageOrVideoAsync(productId, dto, type)
    });

    [HttpDelete("image-delete/{id:long}")]

    public async Task<IActionResult> DeleteImage(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.projectService.DeleteImageOrVideoAsync(id)
        });
}
