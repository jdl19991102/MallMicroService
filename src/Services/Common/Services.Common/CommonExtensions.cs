using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public static class CommonExtensions
    {
        public static WebApplicationBuilder AddServiceDefaults(this WebApplicationBuilder builder)
        {
            
            // Default health checks assume the event bus and self health checks
            builder.Services.AddDefaultHealthChecks(builder.Configuration);

            return builder;
        }
    }
}
