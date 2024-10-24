using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Configurations;
using Portfolio.Domain.Enums;
using Portfolio.Service.DTOs.Assets;
using Portfolio.Service.DTOs.Users;
using Portfolio.Service.Helpers;
using Portfolio.Service.Interfaces;
using Portfolio.WebApi.Models;

namespace Portfolio.WebApi.Controllers;

public class UsersController : BaseController
{
    private readonly IUserService userService;
    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(UserCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.CreateAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> PutAsync(UserUpdateDto dto)
    {
        dto.Id = HtppContextHelper.GetUserId();
    
        return Ok(new Response
           {
               StatusCode = 200,
               Message = "Success",
               Data = await this.userService.UpdateAsync(dto)
           });
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.DeleteAsync(id)
        });

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.GetByIdAsync(id)
        });

    [Authorize(Roles = "Admin, SuperAdmin")]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams @params)
        =>  Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.GetAllAsync(@params)
        });


    [Authorize(Roles = "SuperAdmin")]
    [HttpPatch("upgrade-role")]
    public async Task<IActionResult> UpgradeRoleAsync(long id, UserRole role)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.UpgradeRoleAsync(id, role)
        });

    [HttpPost("image-upload")]
    public async Task<IActionResult> UploadImageAsync(long userId, [FromForm] AssetCreationDto dto, UserUploadType type)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.UploadImageOrVideoAsync(userId, dto, type)
        });

    [HttpDelete("image-delete/{id:long}")]

    public async Task<IActionResult> DeleteImage(long id)
        => Ok( new Response{
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.DeleteImageOrVideoAsync(id)
        });
}
