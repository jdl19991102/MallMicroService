using Orders.Application.Interfaces;
using Orders.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Services
{
    public class OrderService : IOrderService
    {
        public Task<IEnumerable<OrdersViewModel>> GetAllOrders()
        {
            throw new NotImplementedException();
        }
    }
}
