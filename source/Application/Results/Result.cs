namespace LeagueBoss.Application.Results;

public abstract record ResultBase
{
    private List<Exception> _errors = [];
    private protected ResultBase()
    {
    }

    public bool IsSuccess { get; protected set; }
    public bool IsFailure { get; protected set; }

    protected void WithErrors(IEnumerable<Exception> exceptions)
    {
        _errors = exceptions.ToList();
    }
    
    public IReadOnlyCollection<Exception> Errors => _errors.AsReadOnly();
}

public record Result : ResultBase
{
    public static Result Ok() =>
        new()
        {
            IsSuccess = true,
            IsFailure = false
        };

    public static Result Fail() =>
        new()
        {
            IsSuccess = false,
            IsFailure = true
        };

    public static implicit operator Result(Exception ex)
    {
        var result = Fail();
        result.WithErrors([ex]);
        return result;
    }
}

public record Result<T> : ResultBase
{
    public T Value { get; init; } = default!;
    public static Result<T> Ok(T value)
    {
        return new Result<T>()
        {
            IsFailure = false,
            IsSuccess = true,
            Value = value
        };
    }

    public static Result<T> Fail()
    {
        return new Result<T>()
        {
            IsFailure = true,
            IsSuccess = false
        };
    }

    public static implicit operator Result<T>(T value)
    {
        return Ok(value);
    }

    public static implicit operator Result<T>(Exception ex)
    {
        var result = Fail();
        result.WithErrors([ex]);
        return result;
    }
}