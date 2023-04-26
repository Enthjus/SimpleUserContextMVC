using Microsoft.AspNetCore.Authorization;

namespace SimpleUser.MVC.Authorization
{
    public class IsAllowedToDeleteUserRequirement : IAuthorizationRequirement
    {
    }
}
