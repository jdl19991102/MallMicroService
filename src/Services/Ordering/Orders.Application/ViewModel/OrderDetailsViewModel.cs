using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.ViewModel
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public int? OrdersId { get; set; }
        public int? ProductId { get; set; }
        /// <summary>
        /// 子订单价格
        /// </summary>
        public decimal DetailsPrice { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int ProductQuantity { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
