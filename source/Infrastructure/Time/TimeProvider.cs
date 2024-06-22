namespace LeagueBoss.Infrastructure.Time;

using Application.Time;

public class TimeProvider : ITimeProvider
{
    public DateTime Now => DateTimeOffset.UtcNow.DateTime;
    public DateOnly Today => DateOnly.FromDateTime(Now);
}