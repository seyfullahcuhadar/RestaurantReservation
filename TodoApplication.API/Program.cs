using Prometheus;
using TodoApplication.API.Extensions;
using TodoApplication.Application;
using TodoApplication.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapPrometheusScrapingEndpoint();

app.UseMetricServer();
app.UseHttpMetrics();
app.UseHttpsRedirection();
app.UseRequestContextLogging();
app.UseCustomExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapMetrics();
app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.Run();