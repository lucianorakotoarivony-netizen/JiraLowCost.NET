
using JiraLowCost.api.Models.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace JiraLowCost.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController(IUserService userService): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UserResponseDto>>> GetAllUser()
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        List<UserResponseDto> users = await userService.GetAllUserAsync();
        return Ok(users);
    }
}
