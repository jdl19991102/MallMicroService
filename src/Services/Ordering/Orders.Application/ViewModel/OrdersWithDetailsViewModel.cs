using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.ViewModel
{
    public class OrdersWithDetailsViewModel
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int Id { get; set; }
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
        /// 订单详情
        /// </summary>
        public List<OrderDetailsViewModel>? OrderDetails { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
