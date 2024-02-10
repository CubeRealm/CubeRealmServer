using CubeRealm.Network.Packets;
using NetworkAPI.Protocol.Util;

namespace CubeRealmProtocol.Version765.Configuration.ToClient;

public class ClientboundKeepAlive : Packet<ClientboundKeepAlive>, IToClient
{
    public override int PacketId => 0x03;
    
    public long KeepAliveId { get; set; }
    
    public override void Read(IMinecraftStream stream)
    {
        KeepAliveId = stream.ReadLong();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteLong(KeepAliveId);
    }
}