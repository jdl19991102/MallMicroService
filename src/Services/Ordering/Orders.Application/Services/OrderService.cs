using AutoMapper;
using Orders.Application.DTO;
using Orders.Application.Interfaces;
using Orders.Application.ViewModel;
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

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateOrder(CreateOrderDTO createOrderDto)
        {
            // 将createOrderDto 中关于订单的信息抽出来赋值给新对象order
            var order = new OrdersInfo
            {
                OrderName = createOrderDto.OrderName,
                Price = createOrderDto.Price,
                CustomerName = createOrderDto.CustomerName
            };

            // 先新增订单主表
             _orderRepository.CreateOrder(order);




            //var order = _mapper.Map<OrdersInfo>(createOrderDto);
            //return  await _orderRepository.CreateOrder(order);

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
