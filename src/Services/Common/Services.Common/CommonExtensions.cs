using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Services.Common.ServiceExtensions;
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
            // Add the logs
            builder.AddLogs();

            // Add the controllers filter
            builder.Services.AddFiltersConfiguration();

            // Default health checks
            builder.Services.AddHealthChecks();

            // Add the RabbitMQ connection
            builder.Services.AddRabbitMQ(builder.Configuration);

            // Add the event bus
            builder.Services.AddEventBus(builder.Configuration);

            return builder;
        }


        public static WebApplication UseServiceDefaults(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            app.UseHealthChecks("/health");

            return app;
        }
    }
}
