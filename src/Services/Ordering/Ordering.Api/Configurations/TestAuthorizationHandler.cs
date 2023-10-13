using Microsoft.AspNetCore.Authorization;

namespace Ordering.Api.Configurations
{
    public class TestAuthorizationHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var testRequirement = context.Requirements.FirstOrDefault(x => x is CustomRequirement) as CustomRequirement;
            if (testRequirement != null)
            {
                if (testRequirement.myName == "wangwu")
                {
                    Console.WriteLine("进入自定义策略授权01.。。。");
                    context.Succeed(testRequirement);
                }
                else
                {
                    context.Fail();
                }
            }
            var requirement = context.Requirements;
            var user = context.User;
            var resource = context.Resource;
            var pendingRequirements = context.PendingRequirements.ToList();       
            return Task.CompletedTask;  // 直接这么返回，会返回 403
        }
    }
}
