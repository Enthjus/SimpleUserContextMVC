using Microsoft.AspNetCore.Authorization;

namespace SimpleUser.MVC.Authorization
{
    public class IsSuperHandler : AuthorizationHandler<IsAccountSuperAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAccountSuperAdminRequirement requirement)
        {
            if (context.User.HasClaim(f => f.Type == "Super"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
