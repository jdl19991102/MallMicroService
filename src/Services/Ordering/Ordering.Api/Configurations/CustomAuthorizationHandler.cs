using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

                //取出来相关的claims
                var claims = context.User.Claims;
                //取出来相关的角色
                var roles = claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
                // 如果roles包含Admin，那么就通过
                if (roles.Contains("Admin"))
                {
                    flag = true;
                }
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
