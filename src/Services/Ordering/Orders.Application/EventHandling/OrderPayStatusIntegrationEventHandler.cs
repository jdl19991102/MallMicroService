using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Orders.Application.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.EventHandling
{
    public class OrderPayStatusIntegrationEventHandler : IIntegrationEventHandler<OrderPayStatusIntegrationEvent>
    {
        private readonly ILogger<OrderPayStatusIntegrationEventHandler> _logger;
        private readonly IEventBus _eventBus;

        public OrderPayStatusIntegrationEventHandler(ILogger<OrderPayStatusIntegrationEventHandler> logger,
            IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }
        public async Task Handle(OrderPayStatusIntegrationEvent @event)
        {
            //_logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
            //_logger.LogInformation($"OrderPayStatusIntegrationEventHandler handle OrderPayStatusIntegrationEvent: {@event.OrderId}");
            //_eventBus.Publish(new OrderPayStatusChangedToPaidIntegrationEvent(@event.OrderId));
            await Task.CompletedTask;
        }
    }
}
