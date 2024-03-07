namespace World.API.Content.Entity;

public class Zombie : ILivingEntity<Zombie>
{
    public Entities<Zombie> Type { get; }
    public Guid Id { get; }
}