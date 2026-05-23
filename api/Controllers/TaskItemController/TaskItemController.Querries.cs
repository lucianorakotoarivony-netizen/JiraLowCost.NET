using System.Security.Claims;
using JiraLowCost.api.Models.Dtos.TaskItem;
using Microsoft.AspNetCore.Mvc;
namespace JiraLowCost.api.Controllers.TaskItemController;
public partial class TaskItemController
{

    [HttpGet]
    public async Task<ActionResult<List<TaskItemResponseDto>>> GetAllTaskItem([FromQuery] string? filter)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        string? UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(UserId)) return Unauthorized(UnauthorizedAccessException);
        List<TaskItemResponseDto> taskItem = await taskItemService.GetAllTaskItemAsync(UserId, filter);
        return Ok(taskItem);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItemResponseDto>> GetOneTaskItem(int id)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        string? UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(UserId)) return Unauthorized(UnauthorizedAccessException);
        TaskItemResponseDto taskItem = await taskItemService.GetOneTaskItemAsync(UserId, id);
        return Ok(taskItem);
    }
    
}