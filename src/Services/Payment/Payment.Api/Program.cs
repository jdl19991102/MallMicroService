using EventBus.Abstractions;
using Payment.Api.Configurations;
using Payment.Api.Controllers;
using Payment.Api.IntegrationEvents.EventHandling;
using Payment.Api.IntegrationEvents.Events;
using Services.Common;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var services = builder.Services;

services.AddSwagger();
services.AddDbContexts(builder.Configuration);
services.AddTransient<OrderPayStatusIntegrationEventHandler>();
services.AddScoped<PaymentController>();

var app = builder.Build();

app.UseServiceDefaults();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<OrderPayStatusIntegrationEvent, OrderPayStatusIntegrationEventHandler>();

app.UsePaymentService();

app.Run();
