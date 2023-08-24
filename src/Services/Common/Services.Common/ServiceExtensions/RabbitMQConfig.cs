using EventBus.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.ServiceExtensions
{
    public static class RabbitMQConfig
    {
        public static void AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var section = configuration.GetSection("RabbitMQConfig");
            if (section == null) throw new ArgumentNullException(nameof(section));

            if (Convert.ToBoolean(section["Enabled"]))
            {
                services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();

                    var factory = new ConnectionFactory()
                    {
                        HostName = section["Host"],
                        DispatchConsumersAsync = true
                    };

                    if (!string.IsNullOrEmpty(section["UserName"]))
                    {
                        factory.UserName = section["UserName"];
                    }

                    if (!string.IsNullOrEmpty(section["Password"]))
                    {
                        factory.Password = section["Password"];
                    }

                    if (!string.IsNullOrEmpty(section["Port"]))
                    {
                        factory.Port = int.Parse(section["Port"]);
                    }

                    var retryCount = 5;
                    if (!string.IsNullOrEmpty(section["RetryCount"]))
                    {
                        retryCount = int.Parse(section["RetryCount"]);
                    }

                    return new RabbitMQPersistentConnection(factory, logger, retryCount);
                });
            }
        }
    }
}
