using Orders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Interfaces
{
    /// <summary>
    /// 订单仓储接口
    /// </summary>
    public interface IOrderRepository : IRepository<OrdersInfo>
    {
        void CreateOrder(OrdersInfo order);

        Task<IEnumerable<OrdersInfo>> GetAllOrders(Expression<Func<OrdersInfo, bool>> expression);
    }
}
