using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using TodoApplication.API.Extensions;
using TodoApplication.Application;
using TodoApplication.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var serviceName = builder.Configuration.GetSection("SourceName").Value;
builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
        .AddConsoleExporter();
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMetricServer();
app.UseHttpMetrics();
app.UseHttpsRedirection();
app.UseRequestContextLogging();
app.UseCustomExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapMetrics();

app.Run();