namespace LeagueBoss.Domain.Matches;

using Sports;

[StronglyTypedId]
public readonly partial struct MatchId;

public abstract record Match
{
    private protected Match()
    {
    }

    private readonly List<Game> _games = [];
    public MatchId Id { get; init; }
    public required SportId SportId { get; init; }
    public required PlayerId Organiser { get; init; }
    public IReadOnlyCollection<Game> Games => _games.AsReadOnly();
    
    public void AddGames(List<Game> games)
    {
        if (games.Any(x => x.Match != this))
        {
            throw new InvalidGamesForMatchException(games, this);
        }
        
        _games.AddRange(games.Where(g => !_games.Contains(g)));
    }
}