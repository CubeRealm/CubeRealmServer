using NetworkAPI.Protocol;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Status.ToServer;

public class StatusRequest : Packet<StatusRequest>, IToServer
{
    public byte ServerId => 0x00;
    
    public override void Read(IMinecraftStream stream)
    {
        
    }

    public override void Write(IMinecraftStream stream)
    {
        
    }
}