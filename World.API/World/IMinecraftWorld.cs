namespace World.API.World;

public interface IMinecraftWorld : ILevel, ILevelLoader, ILevelGenerator, ILevelNetwork
{
    public Guid Guid { get; }
}