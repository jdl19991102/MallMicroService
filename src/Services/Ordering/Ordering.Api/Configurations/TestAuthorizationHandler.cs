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
                else if (testRequirement.myName == "zhangsan")
                {
                    Console.WriteLine("进入自定义策略授权02.。。。");
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
            //if (testRequirement != null)
            //{
            //    context.Succeed(testRequirement);
            //}
            return Task.CompletedTask;
        }
    }
}
