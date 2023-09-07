using Ordering.Api.Configurations;
using Services.Common;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddSwagger();
builder.Services.AddDbContexts(builder.Configuration);

var app = builder.Build();

app.UseServiceDefaults();

app.UseMySwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
