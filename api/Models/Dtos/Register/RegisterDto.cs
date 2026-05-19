using JiraLowCost.api.Constants;

namespace JiraLowCost.api.Models.Dtos.Register;

public class RegisterDto
{
    public string Username {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Role {get; set;} = UserRole.JUNIOR;
    public string Password {get; set;} = string.Empty;
}