using System.ComponentModel.DataAnnotations;
using JiraLowCost.api.Constants;

namespace JiraLowCost.api.Models.Dtos.TaskItem;

public class CreateTaskItemDto
{
    [Required]
    public string Title {get; set;} = string.Empty;
    public string? Description {get; set;} = string.Empty;
    public string Difficulty {get; set;} = UserRole.SENIOR;
    public string Priority { get; set;} = TaskItemPriority.MEDIUM;
    public string? AssignedToId {get; set;} = string.Empty;
}
public class TaskItemResponseDto
{
    public int Id {get; set;}
    public  string Title {get; set;} = null!;
    public string? Description {get; set;}
    public string Status {get; set;} = null!;
    public string Priority {get; set;} = null!;
    public string Difficulty { get; set;} = null!;
    public string? AssignedTo {get; set;}
    public string CreatedBy {get; set;} = null!;
    public DateTime CreatedAt { get; set; }
}