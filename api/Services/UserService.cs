
using JiraLowCost.api.Data;
using JiraLowCost.api.Models.Dtos.User;
using JiraLowCost.api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JiraLowCost.api.Services;

public class UserService(ApplicationDbContext context) : IUserService
{
    public async Task<List<UserResponseDto>> GetAllUserAsync()
    {
        List<User> users = await context.Users.ToListAsync() ?? [];
        return [.. users.Select(u => new UserResponseDto
        {
            Id = u.Id,
            Username = u.UserName!,
            Role = u.Role,
        })];
    }
}