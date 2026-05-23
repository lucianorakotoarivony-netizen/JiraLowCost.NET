using JiraLowCost.api.Models.Dtos.Auth;
using JiraLowCost.api.Models.Dtos.Login;
using JiraLowCost.api.Models.Dtos.Register;
using JiraLowCost.api.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace JiraLowCost.api.Services;

public class AuthService(UserManager<User> userManager, IConfiguration configuration) : IAuthService
{
    public async Task<AuthResponseLoginDto> LoginAsync(LoginDto login)
    {
        User? user = await userManager.FindByNameAsync(login.Username);
        if (user == null || !await userManager.CheckPasswordAsync(user, login.Password))
        {
            throw new InvalidOperationException("Identifiants invalide");
        }

        string token = GenerateJwtToken(user);
        AuthResponseLoginDto resultDto = new()
        {
            Username = user.UserName!,
            Role = user.Role,
            Token = token
        };
        
        return resultDto;
    }

    public async Task<AuthResponseRegisterDto> RegisterAsync(RegisterDto register)
    {
        User? existingUserByUsername = await userManager.FindByNameAsync(register.Username);
        User? existingUserByEmail = await userManager.FindByEmailAsync(register.Email);
        if (existingUserByUsername != null || existingUserByEmail != null)
        {
            throw new InvalidOperationException("Inscription impossible.");
        }
        User user = new()
        {
            UserName = register.Username,
            Email = register.Email,
            Role = register.Role,
        };
        IdentityResult result = await userManager.CreateAsync(user, register.Password);
        if (!result.Succeeded)
        {
            string? error = string.Join(", ", result.Errors.Select(e=>e.Description));
            throw new InvalidOperationException($"Erreur lors de la création : {error}");
        }
        AuthResponseRegisterDto resultDto = new()
        {
            Username = register.Username,
        };
        return resultDto;
    }

    private string GenerateJwtToken(User user)
    {
        Claim[] claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim("username", user.UserName!),
            new Claim("role", user.Role),
            new Claim(ClaimTypes.Role, user.Role)
        ];
        SymmetricSecurityKey key = new (
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)
        );
        SigningCredentials creds = new (key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new (
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
