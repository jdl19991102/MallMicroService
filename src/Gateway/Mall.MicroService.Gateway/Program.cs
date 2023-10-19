using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ��һ��д��
//builder.Configuration.AddJsonFile("ocelot.json", optional: true, reloadOnChange: true);
//builder.Services.AddOcelot();

builder.Services.AddAuthentication()
    .AddJwtBearer("UserIdentity", options =>
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

// ocelot�м���Ƚ��ʺϷ���UseRouting()�м��֮ǰ, app.UseHttpsRedirection(); ֮��
app.UseOcelot().Wait();

app.UseAuthorization();

app.MapControllers();

app.Run();
