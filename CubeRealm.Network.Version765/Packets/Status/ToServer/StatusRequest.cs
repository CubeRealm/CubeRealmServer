using CubeRealm.Network.Packets;
using NetworkAPI.Protocol.Util;

namespace CubeRealmProtocol.Version765.Status.ToServer;

public class StatusRequest : Packet<StatusRequest>, IToServer
{ 
    public override int PacketId => 0x00;

    public override void Read(IMinecraftStream stream)
    {
        
    }

    public override void Write(IMinecraftStream stream)
    {
        
    }
}