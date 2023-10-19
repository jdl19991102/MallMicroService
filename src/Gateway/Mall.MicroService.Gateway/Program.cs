using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 第一种写法
//builder.Configuration.AddJsonFile("ocelot.json", optional: true, reloadOnChange: true);
//builder.Services.AddOcelot();

builder.Services.AddAuthentication()
    .AddJwtBearer("UserIdentity", options =>
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

builder.Services.AddOcelot(new ConfigurationBuilder()
    .AddJsonFile("ocelot.json", optional: true, reloadOnChange: true).Build());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ocelot中间件比较适合放在UseRouting()中间件之前, app.UseHttpsRedirection(); 之后
app.UseOcelot().Wait();

app.UseAuthorization();

app.MapControllers();

app.Run();
