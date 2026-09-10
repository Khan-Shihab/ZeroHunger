using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    AuthServices authService;
    public AuthController(AuthServices authService)
    {
        this.authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(AuthModel model)
    {
        var result = authService.Register(model);
        if (!result) return BadRequest("Email already registered");
        return Ok("Registered successfully");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginModel model)
    {
        var result = authService.Login(model);
        if (result == null) return Unauthorized("Invalid credentials");
        return Ok(result);
    }
}