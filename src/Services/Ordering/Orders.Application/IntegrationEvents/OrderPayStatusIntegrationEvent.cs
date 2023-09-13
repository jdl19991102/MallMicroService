using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.IntegrationEvents
{
    public class OrderPayStatusIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }

        public OrderPayStatusIntegrationEvent(int orderId)
            => OrderId = orderId;
    }
}
