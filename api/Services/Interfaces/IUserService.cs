
using JiraLowCost.api.Models.Dtos.User;

public interface IUserService
{
    Task<List<UserResponseDto>> GetAllUserAsync();
}