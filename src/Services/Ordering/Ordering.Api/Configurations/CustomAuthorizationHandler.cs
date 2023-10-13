using Microsoft.AspNetCore.Authorization;

namespace Ordering.Api.Configurations
{
    public class CustomAuthorizationHandler : AuthorizationHandler<CustomRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirement requirement)
        {
            bool flag = false;
            if (requirement.myName == "wangwu")
            {
                Console.WriteLine("进入自定义策略授权01...");
                // 执行相关的鉴权逻辑
                flag = true;
            }
            if (requirement.myName == "zhangsan")
            {
                Console.WriteLine("进入自定义策略授权02...");
                // 执行相关的鉴权逻辑               
            }

            if (flag)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask; //验证不同过
        }
    }
}
