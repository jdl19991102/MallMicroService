using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Ordering.Api.Configurations
{
    public static class SwaggerConfig
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

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                Console.WriteLine("xml文件的路径为:{0}", Path.Combine(AppContext.BaseDirectory, xmlFilename));
                // 第二个参数true表示注释文件包含了控制器的注释
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);
            });
        }
    }
}
