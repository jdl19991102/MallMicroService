using Ordering.Api.Configurations;
using Services.Common;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var services = builder.Services;

services.AddSwagger();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
