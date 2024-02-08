using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets;

public interface IPacket
{
    public int PacketId { get; }
    public void Read(IMinecraftStream stream);
    public void Write(IMinecraftStream stream);
}
