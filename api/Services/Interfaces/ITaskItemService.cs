
using JiraLowCost.api.Models.Dtos.TaskItem;

public interface ITaskItemService
{
    Task<TaskItemResponseDto> CreateTaskItemAsync(CreateTaskItemDto dto, string CreatedById);
    Task<List<TaskItemResponseDto>> GetAllTaskItemAsync(string UserId, string filter);
    Task<TaskItemResponseDto> GetOneTaskItemAsync(string UserId, int taskId);
    Task<TaskItemResponseDto> TakeTaskAsync(string userId, int taskId);
    Task<TaskItemResponseDto> AcceptTaskAsync(string userId, int taskId);
    Task<TaskItemResponseDto> DeclineTaskAsync(string userId, int taskId);
    Task<TaskItemResponseDto> AbandonTaskAsync(string userId, int taskId);
    Task<TaskItemResponseDto> FinishTaskAsync(string userId, int taskId);
}