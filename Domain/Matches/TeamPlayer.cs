namespace LeagueBoss.Domain.Matches;



public record TeamPlayer
{
    public PlayerId PlayerId { get; init; }
    public TeamId TeamId { get; init; }
};