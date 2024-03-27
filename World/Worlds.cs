using World.API;
using World.API.World;

namespace World;

public class Worlds : IWorlds
{
    private readonly Dictionary<Guid, IMinecraftWorld> _worlds = new();
    
    public IMinecraftWorld this[Guid id] 
    {
        get
        {
            lock (_worlds)
            {
                return _worlds[id];
            }
        }    
    }

    public async Task LoadWorld(IMinecraftWorld world)
    {
        lock (_worlds)
        {
            if (world.Load())
                _worlds.Add(world.Guid, world);
        }
        
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}