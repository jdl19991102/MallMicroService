using EventBus.Events;

namespace Payment.Api.IntegrationEvents.Events
{
    public class OrderPayStatusIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }

        public OrderPayStatusIntegrationEvent(int orderId)
            => OrderId = orderId;
    }
}
