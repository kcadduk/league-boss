namespace LeagueBoss.Domain.Matches;

public record Frame
{
    public required Game Game { get; init; }
    public required int FrameNumber { get; init; }
    public FrameResult? FrameResult { get; init; }
};