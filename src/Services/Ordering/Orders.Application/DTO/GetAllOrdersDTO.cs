using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.DTO
{
    /// <summary>
    /// 获取所有订单DTO
    /// </summary>
    public class GetAllOrdersDTO
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
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
