using System.Security.Claims;
using JiraLowCost.api.Models.Dtos.TaskItem;
using Microsoft.AspNetCore.Mvc;

namespace JiraLowCost.api.Controllers.TaskItemController;

public partial class TaskItemController
{
    private async Task<ActionResult<T>> ExecuteHttpActionAsync<T>(Func<string, Task<T>> serviceMethod)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized(UnauthorizedOperationException);
        T result = await serviceMethod(userId);
        return Ok(result);
    }

    [HttpPatch("{id}/take")]
    public async Task<ActionResult<TaskItemResponseDto>> TakeTask(int id)
    {
        return await ExecuteHttpActionAsync(userId => taskItemService.TakeTaskAsync(userId, id));
    }

    [HttpPatch("{id}/abandon")]
    public async Task<ActionResult<TaskItemResponseDto>> AbandonTask(int id)
    {
        return await ExecuteHttpActionAsync(userId => taskItemService.AbandonTaskAsync(userId, id));
    }

    [HttpPatch("{id}/finish")]
    public async Task<ActionResult<TaskItemResponseDto>> FinishTask(int id)
    {
        return await ExecuteHttpActionAsync(userId => taskItemService.FinishTaskAsync(userId, id));
    }

    [HttpPatch("{id}/decline")]
    public async Task<ActionResult<TaskItemResponseDto>> DeclineTask(int id)
    {
        return await ExecuteHttpActionAsync(userId => taskItemService.DeclineTaskAsync(userId, id));
    }
    
    [HttpPatch("{id}/accept")]
    public async Task<ActionResult<TaskItemResponseDto>> AcceptTask(int id)
    {
        return await ExecuteHttpActionAsync(userId => taskItemService.AcceptTaskAsync(userId, id));
    }
}