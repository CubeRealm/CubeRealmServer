using NetworkAPI.Protocol.Util;

namespace NetworkAPI.Protocol.Packets.Status.ToServer;

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