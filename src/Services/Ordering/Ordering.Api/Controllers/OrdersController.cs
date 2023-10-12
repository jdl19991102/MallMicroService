using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.DTO;
using Orders.Application.Interfaces;
using Orders.Application.ViewModel;

namespace Ordering.Api.Controllers
{
    /// <summary>
    /// 订单模块
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// 获取所有的订单信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "CustomPolicy")]
        public async Task<IEnumerable<OrdersViewModel>> GetAllOrders([FromBody] GetAllOrdersDTO allOrdersDto)
        {
            var result = await _orderService.GetAllOrders(allOrdersDto);
            return result;
        }


        /// <summary>
        /// 测试权限获取单个订单信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<OrdersViewModel> GetOdersByOrderId(string orderId)
        {
            var result = await _orderService.GetOdersByOrderId(orderId);
            return result;
        }

        /// <summary>
        /// 新增一条订单信息
        /// </summary>
        /// <param name="createOrderDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateOrder([FromBody] CreateOrderDTO createOrderDto)
        {
            var result = await _orderService.CreateOrder(createOrderDto);
            return result;
        }
    }
}
