

using TodoApplication.Application.Abstractions.Clock;

namespace TodoApplication.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}