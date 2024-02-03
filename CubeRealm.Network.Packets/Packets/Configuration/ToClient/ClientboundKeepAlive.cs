using NetworkAPI.Protocol;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Configuration.ToClient;

public class ClientboundKeepAlive : Packet<ClientboundKeepAlive>, IToClient
{
    
    public long KeepAliveId { get; set; }
    public override void Read(IMinecraftStream stream)
    {
        KeepAliveId = stream.ReadLong();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteLong(KeepAliveId);
    }

    public byte ClientId => 0x03;
}