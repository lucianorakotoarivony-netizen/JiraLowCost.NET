using JiraLowCost.api.Constants;
using JiraLowCost.api.Data;
using JiraLowCost.api.Models.Dtos.TaskItem;
using JiraLowCost.api.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace JiraLowCost.api.Services.TaskItemService;

public partial class TaskItemService(ApplicationDbContext context) : ITaskItemService
{
    readonly string [] Role = [UserRole.PO, UserRole.LEAD];

    readonly string UnauthorizedAccessException = "Vous n'avez pas le droit d'acceder à ses ressources.";

    readonly string KeyNotFoundException = "Tâche introuvable.";

    private IQueryable<TaskItem> Query()
    {
        IQueryable<TaskItem> query = context.TaskItems
                                        .Include(t => t.CreatedBy)
                                        .Include(t => t.AssignedTo);
        return query;
    }

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
            CreatedBy = taskItem.CreatedBy?.UserName ?? "Inconnu",
            CreatedAt = taskItem.CreatedAt,
        };
    }
}