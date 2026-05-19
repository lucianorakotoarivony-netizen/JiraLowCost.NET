
using JiraLowCost.api.Models.Dtos.TaskItem;

public interface ITaskItemService
{
    Task<TaskItemResponseDto> CreateTaskItemAsync(CreateTaskItemDto dto, string CreatedById);
    Task<List<TaskItemResponseDto>> GetAllTaskItemAsync(string UserId, string filter);
}