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
            ValidateIssuer = true, //�Ƿ��������ڼ���֤ǩ����
            ValidateAudience = true, //�Ƿ��������ڼ���֤����
            ValidateLifetime = true, //�Ƿ���֤������Ч��
            ValidateIssuerSigningKey = true, //�Ƿ���֤ǩ����Կ
            ValidIssuer = builder.Configuration["Jwt:Issuer"], //������
            ValidAudience = builder.Configuration["Jwt:Audience"], //����
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) //ǩ����Կ
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
