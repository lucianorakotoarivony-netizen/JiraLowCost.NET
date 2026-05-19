using JiraLowCost.api.Constants;
using JiraLowCost.api.Data;
using JiraLowCost.api.Models.Dtos.TaskItem;
using JiraLowCost.api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JiraLowCost.api.Services;

public class TaskItemService(ApplicationDbContext context) : ITaskItemService
{
    private static TaskItemResponseDto ToResponseDto(TaskItem taskItem)
    {
        return new TaskItemResponseDto
        {
            Id = taskItem.Id,
            Title = taskItem.Title,
            Description = taskItem.Description,
            Status = taskItem.Status,
            Priority = taskItem.Priority,
            AssignedTo = taskItem.AssignedTo?.UserName,
            Difficulty = taskItem.Difficulty,
            CreatedBy = taskItem.CreatedBy!.UserName!,
            CreatedAt = taskItem.CreatedAt,
        };
    }
    public async Task<TaskItemResponseDto> CreateTaskItemAsync(CreateTaskItemDto dto, string CreatedBy)
    {
        TaskItem taskItem = new()
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            Difficulty = dto.Difficulty,
            CreatedById = CreatedBy
        };
        if (!string.IsNullOrEmpty(dto.AssignedToId))
        {
            taskItem.AssignedToId = dto.AssignedToId;
            taskItem.Status = TaskItemStatus.PENDING;
        }
        context.Add(taskItem);
        await context.SaveChangesAsync();
        return ToResponseDto(taskItem);
    }


    public async Task<List<TaskItemResponseDto>> GetAllTaskItemAsync(string UserId, string? filter = null)
    {
        User user = await context.Users.FindAsync(UserId) ?? throw new UnauthorizedAccessException("Vous n'avez pas le droit d'accéder à ses ressources.");
        string [] Role = [UserRole.PO, UserRole.LEAD];

        IQueryable<TaskItem> query = context.TaskItems
            .Include(t =>  t.CreatedBy)
            .Include(t => t.AssignedTo);
        if (!Role.Contains(user.Role))
        {
            query = filter switch
            {
                TaskItemStatus.TODO => query.Where(t => t.Status == TaskItemStatus.TODO),
                TaskItemStatus.PENDING => query.Where(t => t.Status == TaskItemStatus.PENDING && t.AssignedToId == UserId),
                TaskItemStatus.IN_PROGRESS => query.Where(t => t.Status == TaskItemStatus.IN_PROGRESS && t.AssignedToId == UserId),
                TaskItemStatus.DONE => query.Where(t => t.Status == TaskItemStatus.DONE),
                _ => query.Where(t =>
                                    t.Status == TaskItemStatus.TODO ||
                                    (t.Status == TaskItemStatus.IN_PROGRESS && t.AssignedToId == UserId) ||
                                    (t.Status == TaskItemStatus.PENDING && t.AssignedToId == UserId) ||
                                    (t.Status == TaskItemStatus.DONE)),
            };
        }
        List<TaskItem> taskItem = await query.ToListAsync();
        
        return [.. taskItem.Select(ToResponseDto)];
    }

}