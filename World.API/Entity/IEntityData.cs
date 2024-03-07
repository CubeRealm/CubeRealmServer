namespace World.API.Entity;

public interface IEntityData
{
    
    int Health { get; set; }
    int MaxHealth { get; set; }
    string Name { get; set; }
    
}