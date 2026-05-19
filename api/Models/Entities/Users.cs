using JiraLowCost.api.Constants;
using Microsoft.AspNetCore.Identity;

namespace JiraLowCost.api.Models.Entities;

public class User : IdentityUser
{
    public string Role {get; set;} = UserRole.JUNIOR;
}