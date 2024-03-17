using World.API.World;

namespace World.API;

public abstract class MinecraftWorld
{

    public Guid Guid { get; }
    
    /// <summary>
    /// World level
    /// </summary>
    public ILevel Level { get; private set; }
    
    /// <summary>
    /// Loader allows to save and load world from file
    /// </summary>
    public IWorldLoader Loader { get; }
    
    /// <summary>
    /// Name of the world
    /// </summary>
    public string WorldName { get; }
    
    public MinecraftWorld(ILevel level, IWorldLoader loader, string worldName)
    {
        Level = level;
        Loader = loader;
        WorldName = worldName;
    }

    public MinecraftWorld(IWorldLoader loader, string worldName)
    {
        Loader = loader;
        WorldName = worldName;
    }

    public virtual void Load()
    {
        Level = Loader.Load(WorldName);
    }

    public virtual void Save()
    {
        Loader.Save(Level);
    }
}