using MediatR;
using Orders.Domain.Interfaces;
using Orders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Orders.Domain.Command.Handler
{
    public class OrderCommandHandler : CommandHandler,
        IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrdersDetailRepository _ordersDetailRepository;

        public OrderCommandHandler(IOrderRepository orderRepository, IOrdersDetailRepository ordersDetailRepository)
        {
            _orderRepository = orderRepository;
            _ordersDetailRepository = ordersDetailRepository;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var order = new OrdersInfo
                    {
                        OrderName = request.OrderName,
                        CustomerName = request.CustomerName,
                        Price = request.Price,
                        OrderUniqueId = Guid.NewGuid().ToString("N")
                    };
                    _orderRepository.CreateOrder(order);
                    await _orderRepository.SaveChangesAsync();
                    var orderId = order.Id;

                    foreach (var item in request.OrderDetails)
                    {
                        item.OrdersId = orderId;
                    }
                    _ordersDetailRepository.CreateOrdersDetailRange(request.OrderDetails);
                    await _ordersDetailRepository.SaveChangesAsync();

                    // 提交事务
                    transactionScope.Complete();
                }
                catch (Exception)
                {
                    // 事务回滚，任何一个步骤出现异常都会导致事务回滚
                    // 这样保证了订单和订单详情要么一起成功，要么一起失败
                    //transactionScope.Dispose(); // 可以不写，using会自动释放
                    return false;
                    throw;
                }
            }

            return true;
        }
    }
}
