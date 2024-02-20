namespace LeagueBoss.Domain.Matches;

using StronglyTypedIds;

[StronglyTypedId]
public partial struct TeamId;
public record Team
{
    public TeamId Id { get; init; }
};