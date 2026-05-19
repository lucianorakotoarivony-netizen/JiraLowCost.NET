
using System.Security.Claims;
using JiraLowCost.api.Constants;
using JiraLowCost.api.Models.Dtos.TaskItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JiraLowCost.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskItemController(ITaskItemService taskItemService): ControllerBase
{
    readonly string [] Role = [UserRole.PO, UserRole.LEAD];
    [HttpPost]
    public async Task<ActionResult<TaskItemResponseDto>> CreateTaskItem([FromBody] CreateTaskItemDto dto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        string? UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        string? CurrentUserRole = User.FindFirstValue(ClaimTypes.Role);
        if (string.IsNullOrEmpty(UserId) || !Role.Contains(CurrentUserRole)) return Unauthorized("Vous n'êtes pas autorisé à effectuer cette opération.");
        var taskItem = await taskItemService.CreateTaskItemAsync(dto, UserId);
        return Ok(taskItem);
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskItemResponseDto>>> GetAllTaskItem([FromQuery] string? filter)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        string? UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(UserId)) return Unauthorized("Vous n'avez pas le droit d'accéder à ses ressources.");
        var taskItem = await taskItemService.GetAllTaskItemAsync(UserId, filter);
        return Ok(taskItem);
    }
}