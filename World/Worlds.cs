using World.API;
using World.API.World;

namespace World;

public class Worlds : IWorlds
{
    private readonly Dictionary<Guid, IMinecraftWorld> _worlds = new();

    public ICollection<Guid> Guids
    {
        get
        {
            lock (_worlds)
            {
                return _worlds.Keys;
            }
        }
    }

    public ICollection<IMinecraftWorld> MinecraftWorlds
    {
        get
        {
            lock (_worlds)
            {
                return _worlds.Values;
            }
        }
    }

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

    public async Task UnloadWorld(IMinecraftWorld world)
    {
        lock (_worlds)
        {
            if (_worlds.Keys.Contains(world.Guid))
            {
                world.Save();
                _worlds.Remove(world.Guid);
            }
        }
    }
    
    public async Task UnloadAll()
    {
        lock (_worlds)
        {
            foreach (var world in _worlds.Values)
            {
                world.Save();
            }
            _worlds.Clear();
        }
    }

    public void Dispose()
    {
        UnloadAll();
    }
}