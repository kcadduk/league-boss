namespace LeagueBoss.Domain.Users;

public class FullNameCannotBeNullOrEmptyException : Exception
{
    public string FullName { get; }

    public FullNameCannotBeNullOrEmptyException(string fullName)
    {
        FullName = fullName;
    }
}