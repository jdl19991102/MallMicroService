using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Interfaces;
using Orders.Application.ViewModel;

namespace Ordering.Api.Controllers
{
    /// <summary>
    /// 订单模块
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
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
        [HttpGet]
        public async Task<IEnumerable<OrdersViewModel>> GetAllOrders()
        {
            var result = await _orderService.GetAllOrders();
            return result;
        }
    }
}
