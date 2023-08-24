using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Extensions
{
    public static class GenericTypeExtensions
    {
        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static string GetGenericTypeName(this Type type)
        {
            string typeName;

            // 如果是泛型类型,返回泛型类型名称,并且把泛型参数名称替换为占位符
            if (type.IsGenericType)
            {
                var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
                typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            }
            else
            {
                // 如果不是泛型类型,直接返回类型名称
                typeName = type.Name;
            }

            return typeName;
        }


        public static string GetGenericTypeName(this object @object)
        {
            return @object.GetType().GetGenericTypeName();
        }
    }
}
