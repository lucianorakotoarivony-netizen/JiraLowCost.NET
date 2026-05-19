
using JiraLowCost.api.Models.Dtos.Auth;
using JiraLowCost.api.Models.Dtos.Login;
using JiraLowCost.api.Models.Dtos.Register;

public interface IAuthService
{
    Task<AuthResponseRegisterDto> RegisterAsync(RegisterDto register);
    Task<AuthResponseLoginDto> LoginAsync(LoginDto login);
}