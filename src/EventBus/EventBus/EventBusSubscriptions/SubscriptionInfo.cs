using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.EventBusSubscriptions
{
    public class SubscriptionInfo
    {
        /// <summary>
        /// 是否动态集成事件处理程序
        /// </summary>
        public bool IsDynamic { get; }
        /// <summary>
        /// 集成事件处理程序类型
        /// </summary>
        public Type HandlerType { get; }
        private SubscriptionInfo(bool isDynamic, Type handlerType)
        {
            IsDynamic = isDynamic;
            HandlerType = handlerType;
        }
        /// <summary>
        /// 创建动态集成事件处理程序
        /// </summary>
        /// <param name="handlerType">处理程序类型</param>
        /// <returns></returns>
        public static SubscriptionInfo Dynamic(Type handlerType)
        {
            return new SubscriptionInfo(isDynamic: true, handlerType: handlerType);
        }
        /// <summary>
        /// 创建集成事件处理程序
        /// </summary>
        /// <param name="handlerType">处理程序类型</param>
        /// <returns></returns>
        public static SubscriptionInfo Typed(Type handlerType)
        {
            return new SubscriptionInfo(isDynamic: false, handlerType: handlerType);
        }
    }
}
