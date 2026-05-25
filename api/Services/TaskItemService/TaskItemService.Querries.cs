using JiraLowCost.api.Constants;
using JiraLowCost.api.Models.Dtos.TaskItem;
using JiraLowCost.api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JiraLowCost.api.Services.TaskItemService;
public partial class TaskItemService
{
    public async Task<List<TaskItemResponseDto>> GetAllTaskItemAsync(string userId, string? filter = null, string? dashboard = null)
    {
        User? user = await context.Users.FindAsync(userId)
        ??
        throw new UnauthorizedAccessException(UnauthorizedAccessException);

        IQueryable<TaskItem> query = Query();
        bool isAdmin = RoleAdmin.Contains(user.Role);
        bool isDevDashboard = dashboard == "dev";
        bool isLeadModeDev = isAdmin && isDevDashboard;

        if (!isAdmin || isLeadModeDev)
        {
            query = query.Where(t =>
                    t.Status == TaskItemStatus.TODO 
                        ||
                    t.AssignedToId == userId &&
                    (t.Status == TaskItemStatus.IN_PROGRESS || t.Status == TaskItemStatus.PENDING)
                        ||
                        
                    (t.Status == TaskItemStatus.DONE));
        }

        if (!string.IsNullOrEmpty(filter))
        {
            query = filter switch
            {
                TaskItemStatus.TODO => query
                .Where(t => t.Status == TaskItemStatus.TODO),

                TaskItemStatus.PENDING => isAdmin && !isLeadModeDev ? query.Where(t => t.Status == TaskItemStatus.PENDING) : 
                query.Where(t =>
                    t.Status == TaskItemStatus.PENDING
                        &&
                    t.AssignedToId == userId),
                TaskItemStatus.IN_PROGRESS => isAdmin && !isLeadModeDev ? query.Where(t => t.Status == TaskItemStatus.IN_PROGRESS):
                query.Where(t =>
                    t.Status == TaskItemStatus.IN_PROGRESS
                        &&
                    t.AssignedToId == userId),
                TaskItemStatus.DONE => isAdmin && !isLeadModeDev ? query.Where(t => t.Status == TaskItemStatus.DONE) :
                query.Where(t => t.Status == TaskItemStatus.DONE && t.AssignedToId == userId),
                _ => query
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

        if (!RoleAdmin.Contains(user.Role))
        {
            query = query.Where(t =>
            t.Status == TaskItemStatus.TODO ||
            t.AssignedToId == userId &&
            (t.Status == TaskItemStatus.IN_PROGRESS || t.Status == TaskItemStatus.PENDING)
            ||
            (t.Status == TaskItemStatus.DONE)
        );
        }
        TaskItem taskItem = await query.FirstOrDefaultAsync(t => t.Id == taskId)
        ??
        throw new KeyNotFoundException(KeyNotFoundException);
        return ToResponseDto(taskItem);
    }
}