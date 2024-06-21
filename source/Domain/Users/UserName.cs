namespace LeagueBoss.Domain.Users;

public record UserName
{
    private UserName()
    {
    }

    public static UserName Create(string fullName, string? alias = default)
    {
        if (string.IsNullOrEmpty(fullName))
        {
            throw new FullNameCannotBeNullOrEmptyException(fullName);
        }
        
        return new UserName { FullName = fullName, Alias = alias };
    }
    public required string FullName { get; init; }
    public string? Alias { get; init; }
};