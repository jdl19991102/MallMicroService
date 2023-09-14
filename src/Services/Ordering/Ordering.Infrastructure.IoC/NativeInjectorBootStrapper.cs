using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.Interfaces;
using Orders.Application.Services;
using Orders.Domain.Command;
using Orders.Domain.Command.Handler;
using Orders.Domain.Interfaces;
using Orders.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.IoC
{
    /// <summary>
    /// 原生依赖注入, 和展示层隔离, 单独的一层
    /// </summary>
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<IOrderService, OrderService>();


            // Infra
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrdersDetailRepository, OrdersDetailRepository>();

            // Domain
            services.AddScoped<IRequestHandler<CreateOrderCommand, bool>, OrderCommandHandler>();
        }
    }
}
