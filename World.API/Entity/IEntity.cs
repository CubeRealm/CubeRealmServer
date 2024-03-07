using World.API.Coords;

namespace World.API.Entity;

public interface IEntity
{
    
    bool Alive { get; set; }
    Location Location { get; set; }
    IEntityData EntityData { get; set; }

    void Spawn(Location location);
    void Spawn();
    void Kill();

}