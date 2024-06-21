namespace LeagueBoss.Domain;

public interface IEntity<T>
{
    public T Id { get; init; }
}