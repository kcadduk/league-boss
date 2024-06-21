namespace LeagueBoss.Domain;

public abstract record Entity
{
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
};