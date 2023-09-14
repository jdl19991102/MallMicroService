using EventBus.Abstractions;
using Payment.Api.Controllers;
using Payment.Api.IntegrationEvents.Events;

namespace Payment.Api.IntegrationEvents.EventHandling
{
    public class OrderPayStatusIntegrationEventHandler : IIntegrationEventHandler<OrderPayStatusIntegrationEvent>
    {

        private readonly IEventBus _eventBus;
        private readonly ILogger<OrderPayStatusIntegrationEventHandler> _logger;
        private readonly PaymentController _paymentController;

        public OrderPayStatusIntegrationEventHandler(IEventBus eventBus, ILogger<OrderPayStatusIntegrationEventHandler> logger, 
            PaymentController paymentController)
        {
            _eventBus = eventBus;
            _logger = logger;
            _paymentController = paymentController;
        }

        public async Task Handle(OrderPayStatusIntegrationEvent @event)
        {
            // 这个集成事件是订单创建后，通知支付服务创建支付信息
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
            try
            {
                var paymentInfo = await _paymentController.CreatePayment(@event.OrderId);
                if (paymentInfo != null)
                {
                    await Task.CompletedTask;
                }
                else
                {
                    throw new Exception("订单创建失败"); // 抛出异常
                }
            }
            catch (Exception ex)
            {
                // 处理订单创建失败的逻辑
                _logger.LogError(ex, "订单创建失败");
                // 其他处理代码...               
            }          
        }
    }
}
