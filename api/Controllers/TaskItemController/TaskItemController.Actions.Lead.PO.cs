
using System.Security.Claims;
using JiraLowCost.api.Models.Dtos.TaskItem;
using Microsoft.AspNetCore.Mvc;

namespace JiraLowCost.api.Controllers.TaskItemController;
public partial class TaskItemController
{
    [HttpPost]
    public async Task<ActionResult<TaskItemResponseDto>> CreateTaskItem([FromBody] CreateTaskItemDto dto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        string? UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        string? CurrentUserRole = User.FindFirstValue(ClaimTypes.Role);
        if (string.IsNullOrEmpty(UserId) || !Role.Contains(CurrentUserRole)) return Unauthorized(UnauthorizedOperationException);
        TaskItemResponseDto taskItem = await taskItemService.CreateTaskItemAsync(dto, UserId);
        return Ok(taskItem);
    }
}