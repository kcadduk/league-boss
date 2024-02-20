namespace LeagueBoss.Domain.Matches;

using Sports;

public sealed record SinglesMatch : Match
{
    private SinglesMatch(){}
    public required PlayerId HomePlayer { get; init; }
    public required PlayerId? AwayPlayer { get; init; }

    public static SinglesMatch Create(PlayerId organizer, PlayerId? homePlayer, PlayerId? awayPlayer, SportId sportId)
    {
        if (homePlayer == awayPlayer && (homePlayer.HasValue || awayPlayer.HasValue))
        {
            throw new CannotPlayYourselfException(homePlayer, awayPlayer);
        }
        
        return new SinglesMatch
        {
            HomePlayer = homePlayer ?? organizer,
            AwayPlayer = awayPlayer,
            SportId = sportId,
            Organiser = organizer
        };
    }

    
};