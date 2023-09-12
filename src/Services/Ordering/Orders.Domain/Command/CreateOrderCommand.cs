using MediatR;
using Orders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Command
{
    public class CreateOrderCommand : IRequest<bool>
    {
        public string OrderName { get; set; }

        public string CustomerName { get; set; }

        public decimal Price { get; set; }

        public List<OrdersDetail> OrderDetails { get; set; }
    }
}
