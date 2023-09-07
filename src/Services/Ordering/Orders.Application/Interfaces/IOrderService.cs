﻿using Orders.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// 获取所有的订单信息
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<OrdersViewModel>> GetAllOrders();
    }
}