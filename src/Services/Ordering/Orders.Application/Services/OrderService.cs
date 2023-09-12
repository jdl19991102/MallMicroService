using AutoMapper;
using MediatR;
using Orders.Application.DTO;
using Orders.Application.Interfaces;
using Orders.Application.ViewModel;
using Orders.Domain.Command;
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
        
        public OrderService(IOrderRepository orderRepository, IMapper mapper, IMediator mediator)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<bool> CreateOrder(CreateOrderDTO createOrderDto)
        {
            var createOrderCommand = _mapper.Map<CreateOrderCommand>(createOrderDto);
            var createOrderResult = await _mediator.Send(createOrderCommand);
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
