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

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IMediator mediator, IEventBus eventBus)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _mediator = mediator;
            _eventBus = eventBus;
        }

        public async Task<bool> CreateOrder(CreateOrderDTO createOrderDto)
        {
            // 1. 先创建订单
            var createOrderCommand = _mapper.Map<CreateOrderCommand>(createOrderDto);
            var createOrderResult = await _mediator.Send(createOrderCommand);

            // 2. 订单创建成功后，发布订单创建成功的事件
            if (createOrderResult)
            {
                var order = await _orderRepository.GetOrderByUniqueId(createOrderDto.OrderName!);
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
