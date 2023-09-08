using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.DTO
{
    /// <summary>
    /// 订单详情DTO
    /// </summary>
    public class OrderDetailsDTO
    {
        /// <summary>
        /// 订单表主键
        /// </summary>
        public int OrdersId { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 子订单价格
        /// </summary>
        public decimal DetailsPrice { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int ProductQuantity { get; set; }
    }
}
