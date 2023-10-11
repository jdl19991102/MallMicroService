using Identity.Api.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Identity.Api.Configurations
{
    public static class Extensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Identity API",
                    Version = "v1",
                    Description = "The Identity Service HTTP API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Donglin",
                        Email = "1592776258@qq.com",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://example.com/license")
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                Console.WriteLine("xml文件的路径为:{0}", Path.Combine(AppContext.BaseDirectory, xmlFilename));
                // 第二个参数true表示注释文件包含了控制器的注释
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);

                //var xmlViewModelFilename = $"{typeof(OrdersViewModel).Assembly.GetName().Name}.xml";
                //Console.WriteLine("ViewModel的xml文件的路径为:{0}", Path.Combine(AppContext.BaseDirectory, xmlViewModelFilename));
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlViewModelFilename));
            });
        }

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
