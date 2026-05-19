using JiraLowCost.api.Models.Dtos.Auth;
using JiraLowCost.api.Models.Dtos.Login;
using JiraLowCost.api.Models.Dtos.Register;
using Microsoft.AspNetCore.Mvc;

namespace JiraLowCost.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService): ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseRegisterDto>> Register([FromBody] RegisterDto dto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        AuthResponseRegisterDto result = await authService.RegisterAsync(dto);
        return Ok(result);

    }
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseLoginDto>> Login([FromBody] LoginDto dto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        AuthResponseLoginDto result = await authService.LoginAsync(dto);
        return Ok(result);
    }
}