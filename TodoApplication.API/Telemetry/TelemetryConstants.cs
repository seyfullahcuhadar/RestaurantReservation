using System.Diagnostics;

namespace TodoApplication.API.Telemetry;

public static class TelemetryConstants
{
    public static readonly ActivitySource ActivitySource = new("TodoApplication.API");
} 