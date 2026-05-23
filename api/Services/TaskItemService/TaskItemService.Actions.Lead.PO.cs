using JiraLowCost.api.Constants;
using JiraLowCost.api.Models.Dtos.TaskItem;
using JiraLowCost.api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JiraLowCost.api.Services.TaskItemService;

public partial class TaskItemService
{
    public async Task<TaskItemResponseDto> CreateTaskItemAsync(CreateTaskItemDto dto, string CreatedBy)
    {
        List<string> usersId = await context.Users.Select(u => u.Id).ToListAsync();
        TaskItem taskItem = new()
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            Difficulty = dto.Difficulty,
            CreatedById = CreatedBy
        };
        if (!string.IsNullOrEmpty(dto.AssignedToId) && usersId.Contains(dto.AssignedToId))
        {
            taskItem.AssignedToId = dto.AssignedToId;
            taskItem.Status = TaskItemStatus.PENDING;
        }
        context.Add(taskItem);
        await context.SaveChangesAsync();
        return ToResponseDto(taskItem);
    }
}