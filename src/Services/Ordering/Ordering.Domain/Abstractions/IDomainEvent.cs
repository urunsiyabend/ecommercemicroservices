using MediatR;

namespace Ordering.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
        Guid EventId => Guid.NewGuid();
        DateTime OccurredOn => DateTime.UtcNow;
        public String EventType => GetType().AssemblyQualifiedName;
    }
}
