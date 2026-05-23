using JiraLowCost.api.Constants;
using JiraLowCost.api.Models.Dtos.TaskItem;
using JiraLowCost.api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JiraLowCost.api.Services.TaskItemService;
public partial class TaskItemService
{
    public async Task<List<TaskItemResponseDto>> GetAllTaskItemAsync(string UserId, string? filter = null)
    {
        User? user = await context.Users.FindAsync(UserId)
        ??
        throw new UnauthorizedAccessException(UnauthorizedAccessException);

        IQueryable<TaskItem> query = Query();

        if (!Role.Contains(user.Role))
        {
            query = filter switch
            {
                TaskItemStatus.TODO => query
                .Where(t => t.Status == TaskItemStatus.TODO),
                TaskItemStatus.PENDING => query
                .Where(t =>
                    t.Status == TaskItemStatus.PENDING
                        &&
                    t.AssignedToId == UserId),
                TaskItemStatus.IN_PROGRESS => query
                .Where(t =>
                    t.Status == TaskItemStatus.IN_PROGRESS
                        &&
                    t.AssignedToId == UserId),
                TaskItemStatus.DONE => query
                .Where(t => t.Status == TaskItemStatus.DONE),
                _ => query.Where(t =>
                    t.Status == TaskItemStatus.TODO 
                        ||
                    (t.Status == TaskItemStatus.IN_PROGRESS && t.AssignedToId == UserId)
                        ||
                    (t.Status == TaskItemStatus.PENDING && t.AssignedToId == UserId)
                        ||
                    (t.Status == TaskItemStatus.DONE)),
            };
        }
        List<TaskItem> taskItem = await query.ToListAsync();
        
        return [.. taskItem.Select(ToResponseDto)];
    }

    public async Task<TaskItemResponseDto> GetOneTaskItemAsync(string userId, int taskId)
    {
        User? user = await context.Users.FindAsync(userId)
        ??
        throw new UnauthorizedAccessException(UnauthorizedAccessException);

        IQueryable<TaskItem> query = Query();

        if (!Role.Contains(user.Role))
        {
            query = query.Where(t =>
            t.Status == TaskItemStatus.TODO ||
            (t.Status == TaskItemStatus.IN_PROGRESS && t.AssignedToId == userId) ||
            (t.Status == TaskItemStatus.PENDING && t.AssignedToId == userId) ||
            (t.Status == TaskItemStatus.DONE)
        );
        }
        TaskItem taskItem = await query.FirstOrDefaultAsync(t => t.Id == taskId)
        ??
        throw new KeyNotFoundException(KeyNotFoundException);
        return ToResponseDto(taskItem);
    }
}