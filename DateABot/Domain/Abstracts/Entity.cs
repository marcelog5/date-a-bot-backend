namespace Domain.Abstracts
{
    public abstract class Entity
    {
        protected Entity()
        {
        }

        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; protected set; }
        public bool Active { get; protected set; } = true;
    }
}
