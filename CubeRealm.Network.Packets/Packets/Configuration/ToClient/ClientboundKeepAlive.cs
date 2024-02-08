using NetworkAPI.Protocol;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Configuration.ToClient;

public class ClientboundKeepAlive : IPacket, IToClient
{
    public int PacketId => 0x03;
    
    public long KeepAliveId { get; set; }
    
    public void Read(IMinecraftStream stream)
    {
        KeepAliveId = stream.ReadLong();
    }

    public void Write(IMinecraftStream stream)
    {
        stream.WriteLong(KeepAliveId);
    }
}