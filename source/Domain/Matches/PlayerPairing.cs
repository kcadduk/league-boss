namespace LeagueBoss.Domain.Matches;

public record PlayerPairing
{
    public required PlayerId PlayerOne { get; init; }
    public required PlayerId PlayerTwo { get; init; }
}