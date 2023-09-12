using Orders.Domain.Interfaces;
using Orders.Domain.Models;
using Orders.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Repository
{
    public class OrdersDetailRepository : Repository<OrdersDetail>, IOrdersDetailRepository
    {
        public OrdersDetailRepository(OrderingContext context) : base(context)
        {
        }

        public void CreateOrdersDetailRange(IEnumerable<OrdersDetail> ordersDetails)
        {
            InsertRange(ordersDetails);
        }
    }
}
