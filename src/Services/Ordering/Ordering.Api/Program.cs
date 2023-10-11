using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ordering.Api.Configurations;
using Services.Common;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var services = builder.Services;

services.AddSwagger();
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, //是否在令牌期间验证签发者
            ValidateAudience = true, //是否在令牌期间验证受众
            ValidateLifetime = true, //是否验证令牌有效期
            ValidateIssuerSigningKey = true, //是否验证签名密钥
            ValidIssuer = builder.Configuration["Jwt:Issuer"], //发行人
            ValidAudience = builder.Configuration["Jwt:Audience"], //受众
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) //签名密钥
        };
    });
services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});
services.AddDbContexts(builder.Configuration);
services.AddDependencyInjectionConfiguration();
services.AddAutoMapperConfiguration();
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

app.UseServiceDefaults();

app.UseMySwagger();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
