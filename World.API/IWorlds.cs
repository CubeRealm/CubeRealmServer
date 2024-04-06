using World.API.World;

namespace World.API;

public interface IWorlds : IDisposable
{
    public ICollection<Guid> Guids { get; }
    public ICollection<IMinecraftWorld> MinecraftWorlds { get; }
    public IMinecraftWorld this[Guid id] { get; }
    
    public Task LoadWorld(IMinecraftWorld world);
    public Task UnloadWorld(IMinecraftWorld world);
}