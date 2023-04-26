using Microsoft.AspNetCore.Authorization;

namespace SimpleUser.MVC.Authorization
{
    public class IsAdminHandler : AuthorizationHandler<IsAllowedToDeleteUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAllowedToDeleteUserRequirement requirement)
        {
            if(context.User.HasClaim(f => f.Type == "Admin"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
