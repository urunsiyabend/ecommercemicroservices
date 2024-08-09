namespace Ordering.Domain.Abstractions
{
    public interface IAggregate<T> : IEntity<T>
    {
    }

    public interface IAggregate: IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents();
    }
}
