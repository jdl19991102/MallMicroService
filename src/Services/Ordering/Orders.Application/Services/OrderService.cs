using AutoMapper;
using EventBus.Abstractions;
using MediatR;
using Orders.Application.DTO;
using Orders.Application.IntegrationEvents;
using Orders.Application.Interfaces;
using Orders.Application.ViewModel;
using Orders.Domain.Command;
using Orders.Domain.Exceptions;
using Orders.Domain.Interfaces;
using Orders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IEventBus _eventBus;
        private readonly IProductClient _productClient;


        public OrderService(IOrderRepository orderRepository, IMapper mapper, IMediator mediator, IEventBus eventBus, IProductClient productClient)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _mediator = mediator;
            _eventBus = eventBus;
            _productClient = productClient;
        }

        public async Task<bool> CreateOrder(CreateOrderDTO createOrderDto)
        {
            if(createOrderDto == null)
            {
                throw new OrderingDomainException(3, "创建订单时CreateOrderDTO为空");
            }
            
            if(createOrderDto.OrderDetails == null || !createOrderDto.OrderDetails.Any())
            {
                throw new OrderingDomainException(3, "创建订单时OrderDetails为空");
            }

            // 1. 先扣减库存          
            List<DecreaseStockDTO> decreases = createOrderDto.OrderDetails.Select(x => new DecreaseStockDTO
            {
                CatalogItemId = x.ProductId,
                Quantity = x.ProductQuantity
            }).ToList();
            var decreaseStockResult = await _productClient.DecreaseStock(decreases);
            if (!decreaseStockResult)
            {
                throw new OrderingDomainException(3, "创建订单时扣减库存失败");
            }

            // 2. 先创建订单
            var createOrderCommand = _mapper.Map<CreateOrderCommand>(createOrderDto);
            var createOrderResult = await _mediator.Send(createOrderCommand);

            // 3. 订单创建成功后，发布订单创建成功的事件
            if (createOrderResult)
            {
                var order = await _orderRepository.GetOrderByOrderName(createOrderDto.OrderName!);
                if (order == null)
                {
                    throw new OrderingDomainException(3, "订单创建成功之后没有查到数据");
                }
                else
                {
                    // 发送集成事件
                    var orderCreatedEvent = new OrderPayStatusIntegrationEvent(order.Id);
                    _eventBus.Publish(orderCreatedEvent);
                }
            }
            return true;
        }

        public async Task<IEnumerable<OrdersViewModel>> GetAllOrders(GetAllOrdersDTO allOrdersDto)
        {
            var ordersList = await _orderRepository.GetAllOrders(x => x.OrderName == allOrdersDto.OrderName);
            var result = _mapper.Map<IEnumerable<OrdersViewModel>>(ordersList);
            return result;
        }
    }
}
