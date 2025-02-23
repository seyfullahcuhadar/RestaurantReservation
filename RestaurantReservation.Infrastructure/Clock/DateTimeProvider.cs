using System;
using RestaurantReservation.Application.Abstractions.Clock;

namespace RestaurantReservation.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}