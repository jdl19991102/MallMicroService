using System;
using System.Collections.Generic;

namespace Orders.Domain.Models
{
    public partial class OrdersInfo
    {
        public int Id { get; set; }
        public string OrderName { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string OrderUniqueId { get; set; } = null!;
    }
}
