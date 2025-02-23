using System.Diagnostics;

namespace RestaurantReservation.API.Telemetry;

public static class TelemetryConstants
{
    public static readonly ActivitySource ActivitySource = new("RestaurantReservation.API");
} 