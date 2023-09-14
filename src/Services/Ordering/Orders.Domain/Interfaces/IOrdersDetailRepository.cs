using Orders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Interfaces
{
    public interface IOrdersDetailRepository : IRepository<OrdersDetail>
    {
        void CreateOrdersDetailRange(IEnumerable<OrdersDetail> ordersDetails);
        Task<int> SaveChangesAsync();
    }
}
