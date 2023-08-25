﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            app.UseHealthChecks("/health");

            return app;
        }
    }
}
