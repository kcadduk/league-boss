namespace LeagueBoss.Application.Time;

public interface ITimeProvider
{
    public DateTime Now { get; }
    public DateOnly Today { get; }
}