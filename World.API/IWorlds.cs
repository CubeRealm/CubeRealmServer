using World.API.World;

namespace World.API;

public interface IWorlds : IDisposable
{
    public IMinecraftWorld this[Guid id] { get; }
    
    public Task LoadWorld(IMinecraftWorld world);
}