namespace LeagueBoss.Domain.Matches;

[StronglyTypedId]
public readonly partial struct PlayerId;

public record Player
{
    private Player(){}
    public PlayerId Id { get; init; }
};