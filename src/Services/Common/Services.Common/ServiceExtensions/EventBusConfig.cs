using EventBus.Abstractions;
using EventBus.EventBusSubscriptions;
using EventBus.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.ServiceExtensions
{
    public static class EventBusConfig
    {
        public static void AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {

            if (services == null) throw new ArgumentNullException(nameof(services));

            var section = configuration.GetSection("EventBus");
            if (section == null) throw new ArgumentNullException(nameof(section));

            if (Convert.ToBoolean(section["Enabled"]))
            {
                services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
                {
                    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                    var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                    var subscriptionClientName = section["SubscriptionClientName"];

                    var retryCount = 5;
                    if (!string.IsNullOrEmpty(section["RetryCount"]))
                    {
                        retryCount = int.Parse(section["RetryCount"]);
                    }

                    return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubscriptionsManager, subscriptionClientName, retryCount);
                });
            };

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            //services.AddTransient<StorageDecreaseIntegrationEventHandler>();
        }
    }
}
