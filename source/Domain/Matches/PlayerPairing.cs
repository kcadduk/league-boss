namespace LeagueBoss.Domain.Matches;

public record PlayerPairing
{
    public required PlayerId PlayerOne { get; init; }
    public required PlayerId PlayerTwo { get; init; }
}

public static class PlayerPairingExtensions 
{
    public static bool HasIntersectedPlayers(this PlayerPairing first, PlayerPairing second)
    {
        return first.PlayerOne == second.PlayerOne || first.PlayerOne == second.PlayerTwo ||
               first.PlayerTwo == second.PlayerOne || first.PlayerTwo == second.PlayerTwo;
    }
}