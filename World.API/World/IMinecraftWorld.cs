namespace World.API.World;

public interface IMinecraftWorld : ILevel, ILevelLoader, ILevelGenerator, ILevelNetwork, IDisposable
{
    public Guid Guid { get; }
}
