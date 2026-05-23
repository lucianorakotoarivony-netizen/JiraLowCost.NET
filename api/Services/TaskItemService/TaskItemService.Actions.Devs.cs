using System.Reflection.Metadata.Ecma335;
using JiraLowCost.api.Constants;
using JiraLowCost.api.Models.Dtos.TaskItem;
using JiraLowCost.api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JiraLowCost.api.Services.TaskItemService;

public partial class TaskItemService
{
    public async Task<TaskItemResponseDto> TakeTaskAsync(string userId, int taskId)
    {
        User? user = await context.Users.FindAsync(userId)
        ??
        throw new UnauthorizedAccessException(UnauthorizedAccessException);

        TaskItem taskItem = await Query()
            .Where(t =>
                t.Difficulty == user.Role
                    &&
                t.AssignedToId == null
                    &&
                t.Status == TaskItemStatus.TODO)
            .FirstOrDefaultAsync(t=> t.Id == taskId)
            ??
        throw new KeyNotFoundException(KeyNotFoundException);

        taskItem.AssignedToId = userId;
        taskItem.Status = TaskItemStatus.IN_PROGRESS;
        await context.SaveChangesAsync();
        return ToResponseDto(taskItem);
    }
    public async Task<TaskItemResponseDto> AcceptTaskAsync(string userId, int taskId)
    {
        User? user = await context.Users.FindAsync(userId)
            ??
        throw new UnauthorizedAccessException(UnauthorizedAccessException);

        TaskItem taskItem = await Query()
            .Where(t =>
                t.Difficulty == user.Role
                    &&
                t.Status == TaskItemStatus.PENDING
                    &&
                t.AssignedToId == userId)
            .FirstOrDefaultAsync(t => t.Id == taskId)
            ??
        throw new KeyNotFoundException(KeyNotFoundException);
        taskItem.Status = TaskItemStatus.IN_PROGRESS;
        await context.SaveChangesAsync();
        return ToResponseDto(taskItem);
    }
    public async Task<TaskItemResponseDto> DeclineTaskAsync(string userId, int taskId)
    {
        User? user = await context.Users.FindAsync(userId)
        ??
        throw new UnauthorizedAccessException(UnauthorizedAccessException);

        TaskItem taskItem = await Query()
            .Where(t =>
                t.AssignedToId == userId
                    &&
                t.Status == TaskItemStatus.PENDING)
            .FirstOrDefaultAsync(t => t.Id == taskId)
        ??
        throw new KeyNotFoundException(KeyNotFoundException);
        
        taskItem.AssignedToId = null;
        taskItem.Status = TaskItemStatus.TODO;
        await context.SaveChangesAsync();

        return ToResponseDto(taskItem);
    }

    public async Task<TaskItemResponseDto> AbandonTaskAsync(string userId, int taskId)
    {
        User? user = await context.Users.FindAsync(userId)
        ??
        throw new UnauthorizedAccessException(UnauthorizedAccessException);

        TaskItem taskItem = await Query()
            .Where(t =>
                t.AssignedToId == userId
                    &&
                t.Status == TaskItemStatus.IN_PROGRESS)
            .FirstOrDefaultAsync(t => t.Id == taskId)
        ??
        throw new KeyNotFoundException(KeyNotFoundException);
        
        taskItem.AssignedToId = null;
        taskItem.Status = TaskItemStatus.TODO;
        await context.SaveChangesAsync();

        return ToResponseDto(taskItem);
    }
    public async Task<TaskItemResponseDto> FinishTaskAsync(string userId, int taskId)
    {
        User? user = await context.Users.FindAsync(userId) 
        ??
        throw new UnauthorizedAccessException(UnauthorizedAccessException);

        TaskItem taskItem = await Query()
            .Where(t =>
                t.AssignedToId == userId
                    &&
                t.Status == TaskItemStatus.IN_PROGRESS)
            .FirstOrDefaultAsync(t => t.Id == taskId)
        ??
        throw new KeyNotFoundException(KeyNotFoundException);
        taskItem.Status = TaskItemStatus.DONE;
        await context.SaveChangesAsync();

        return ToResponseDto(taskItem);
    }
}