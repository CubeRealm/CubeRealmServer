namespace World.API.Content.Entity;

public interface IEntity<T> where T : class, IEntity<T>, new()
{
    public Entities<T> Type { get; }
    public Guid Id { get; }
}

public interface ILivingEntity<T> : IEntity<T> where T : class, ILivingEntity<T>, new();