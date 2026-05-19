using JiraLowCost.api.Constants;

namespace JiraLowCost.api.Models.Entities;

public class TaskItem
{
    public int Id {get; set;}
    public required string Title {get; set;} = string.Empty;
    public string? Description {get; set;} = string.Empty;
    public string Status {get; set;} = TaskItemStatus.TODO;
    public string Priority {get; set;} = TaskItemPriority.MEDIUM;
    public string Difficulty { get; set;} = UserRole.SENIOR;
    public string? AssignedToId {get; set;}
    public User? AssignedTo { get; set;}
    public string? CreatedById { get; set; }
    public User? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}