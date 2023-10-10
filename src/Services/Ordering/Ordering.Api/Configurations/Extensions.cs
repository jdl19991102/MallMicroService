using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Ordering.Infrastructure.IoC;
using Orders.Application.AutoMapper;
using Orders.Application.ViewModel;
using Orders.Infrastructure.Context;
using System.Reflection;

namespace Ordering.Api.Configurations
{
    internal static class Extensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Ordering API",
                    Version = "v1",
                    Description = "The Ordering Service HTTP API",
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

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header, //jwt默认存放Authorization信息的位置(请求头中)
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization", //jwt默认的参数名称                
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    // 指定某一个请求用到了哪些安全方案
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer", // 这个名字要和上面 AddSecurityDefinition 里面的名字一样
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                Console.WriteLine("xml文件的路径为:{0}", Path.Combine(AppContext.BaseDirectory, xmlFilename));
                // 第二个参数true表示注释文件包含了控制器的注释
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);

                var xmlViewModelFilename = $"{typeof(OrdersViewModel).Assembly.GetName().Name}.xml";
                Console.WriteLine("ViewModel的xml文件的路径为:{0}", Path.Combine(AppContext.BaseDirectory, xmlViewModelFilename));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlViewModelFilename));
            });
        }


        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderingContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        // 指定数据库迁移的程序集名称，以便在执行迁移时使用正确的程序集。
                        //sqlOptions.MigrationsAssembly(typeof(OrderingContext).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);
                        // maxRetryCount表示最大重试次数，maxRetryDelay表示每次重试之间的最大延迟时间间隔，
                        // errorNumbersToAdd表示要添加到重试策略的SQL Server错误号码列表（如果为null，则使用默认的SQL Server错误号码列表）
                        //sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            });

            return services;
        }


        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services);
        }


        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));
        }


        public static void UseMySwagger(this WebApplication app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                //app.UseSwaggerUI(options =>
                //{
                //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering API V1");
                //    options.RoutePrefix = string.Empty;
                //});
            }
        }
    }
}
