namespace LeagueBoss.Domain.Sports;

[StronglyTypedId]
public readonly partial struct SportId;

public abstract record Sport
{
    public SportId Id { get; init; }
};