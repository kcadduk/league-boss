namespace LeagueBoss.Domain.Matches;

[StronglyTypedId]
public readonly partial struct GameId;

public abstract record Game
{
    private protected Game() {}
    
    private readonly List<Frame> _frames = [];
    public GameId Id { get; init; }
    public required Match Match { get; init; }
    
    public int GameNumber { get; init; }
    
    public IReadOnlyCollection<Frame> Frames => _frames.AsReadOnly();

    internal void AddFrames(int frameCount)
    {
        if (_frames.Any(x => x.FrameResult is not null))
        {
            throw new CannotAddFramesToGameWithFrameResultsException(this);
        }
        
        _frames.Clear();
        
        var frames = Enumerable.Range(0, frameCount).Select(x => new Frame()
            {
                Game = this,
                FrameNumber = x + 1,
            })
            .ToList();
        
        _frames.AddRange(frames);
    }
};