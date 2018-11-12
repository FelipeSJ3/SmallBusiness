using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmallBusiness.Services
{
    public class UserAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            string resource)
        {
            
            if (!context.User.HasClaim(c => c.Type == "SellerId"))
            {
                return Task.CompletedTask;
            }

            var SellerId = context.User.FindFirst(c => c.Type == "SellerId").Value;

            if (SellerId == resource.ToString()) context.Succeed(context.PendingRequirements.FirstOrDefault());
            return Task.CompletedTask;
        }
    }

    public class SameSellerRequirement : IAuthorizationRequirement { }

    public static class Operations
    {
        public static OperationAuthorizationRequirement Create =
            new OperationAuthorizationRequirement { Name = nameof(Create) };
        public static OperationAuthorizationRequirement Read =
            new OperationAuthorizationRequirement { Name = nameof(Read) };
        public static OperationAuthorizationRequirement Update =
            new OperationAuthorizationRequirement { Name = nameof(Update) };
        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement { Name = nameof(Delete) };
    }
}