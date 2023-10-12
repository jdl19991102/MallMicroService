using Microsoft.AspNetCore.Authorization;

namespace Ordering.Api.Configurations
{
    public class CustomRequirement : IAuthorizationRequirement
    {
        public string myName { get; set; }

        public CustomRequirement(string name)
        {
            myName = name;
        }
    }
}
