using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Base.API.PacketsBase;

public abstract class Packet<T> : IPacket where T : Packet<T>, new()
{
    public abstract int PacketId { get; }
    public abstract void Read(IMinecraftStream stream);
    public abstract void Write(IMinecraftStream stream);

    public object CreateNew()
    {
        return CreateNewPacket();
    }

    private T CreateNewPacket()
    {
        return new T();
    }
}

public interface IPacket
{
    int PacketId { get; }
    void Read(IMinecraftStream stream);
    void Write(IMinecraftStream stream);

    object CreateNew();
}
