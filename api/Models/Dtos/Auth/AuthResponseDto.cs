namespace JiraLowCost.api.Models.Dtos.Auth;

public class AuthResponseLoginDto
{
    public string Username {get; set;} = string.Empty;
    public string Token {get; set;} = string.Empty;
    public string Role {get; set;} = string.Empty;
}

public class AuthResponseRegisterDto
{
    public string Username {get; set;} = string.Empty;
}