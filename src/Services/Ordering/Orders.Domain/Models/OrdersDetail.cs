using System;
using System.Collections.Generic;

namespace Orders.Domain.Models
{
    public partial class OrdersDetail
    {
        public int Id { get; set; }
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
