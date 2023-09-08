using Orders.Domain.Interfaces;
using Orders.Domain.Models;
using Orders.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Repository
{
    public class OrderRepository : Repository<OrdersInfo>, IOrderRepository
    {
        public OrderRepository(OrderingContext context) : base(context)
        {
        }

        public void CreateOrder(OrdersInfo order)
        {
            Insert(order);
            //return await Db.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<OrdersInfo>> GetAllOrders(Expression<Func<OrdersInfo, bool>> expression)
        {
            return await SelectListAsync(expression);
        }
    }
}
