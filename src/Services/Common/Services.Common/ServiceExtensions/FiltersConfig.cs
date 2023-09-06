using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Services.Common.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.ServiceExtensions
{
    public static class FiltersConfig
    {
        public static void AddFiltersConfiguration(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddControllers(options =>
            {
                // 添加全局过滤器
                options.Filters.Add(typeof(CommonResultFilter));
                options.Filters.Add(typeof(GlobalExceptionFilterAsync));
            }).AddNewtonsoftJson(options =>
            {
                // 不使用驼峰，返回：UserName
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //忽略NULL值
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //日期序列化格式
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;//指定如何处理日期和时间的时区。我们将其设置为本地时间，即上海时间。
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";//指定日期的格式。
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                // 自定义验证失败返回结果
                options.InvalidModelStateResponseFactory = context =>
                {
                    var result = new
                    {
                        Code = 1,
                        Data = context.ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)),
                        Message = "参数错误"
                    };
                    return new BadRequestObjectResult(result);
                };
            });
        }
    }
}
