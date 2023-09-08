using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.DTO
{
    /// <summary>
    /// 新增订单DTO
    /// </summary>
    public class CreateOrderDTO
    {
        /// <summary>
        /// 订单名字
        /// </summary>
        public string? OrderName { get; set; }
        /// <summary>
        /// 下单用户名字
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// 订单价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 订单详情集合
        /// </summary>

        public List<OrderDetailsDTO>? OrderDetails { get; set; }
    }
}
