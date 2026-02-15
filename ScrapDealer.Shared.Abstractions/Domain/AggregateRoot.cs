namespace ScrapDealer.Shared.Abstractions.Domain
{
    public abstract class AggregateRoot<TId> : Entity<TId>, ISoftDeletable
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        protected AggregateRoot() { }

        protected AggregateRoot(TId id) : base(id) { }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public bool IsDeleted { get; private set; }

        public void AddDomainEvent(IDomainEvent domainEvent)
            => _domainEvents.Add(domainEvent);

        public void RemoveDomainEvent(IDomainEvent domainEvent)
            => _domainEvents.Remove(domainEvent);

        public void ClearDomainEvents()
            => _domainEvents.Clear();

        public void SoftDelete()
            => IsDeleted = true;
    }
}
