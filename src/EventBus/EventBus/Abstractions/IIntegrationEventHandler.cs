using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
      where TIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event">事件模型</param>
        /// <returns></returns>
        Task Handle(TIntegrationEvent @event);
    }

    /// <summary>
    /// 集成事件处理程序基接口
    /// </summary>
    public interface IIntegrationEventHandler
    {
    }
}
