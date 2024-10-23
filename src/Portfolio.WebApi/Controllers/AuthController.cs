using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Service.DTOs.Users;
using Portfolio.Service.Interfaces;
using Portfolio.WebApi.Models;

namespace Portfolio.WebApi.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService authService;
    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> GenerateTokenAsync([FromBody] LoginRequest login)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.authService.GenerateTokenAsync(login.UserName, login.Password)
        });
    }
}
