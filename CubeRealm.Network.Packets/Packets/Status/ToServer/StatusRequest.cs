using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Status.ToServer;

public class StatusRequest : IPacket, IToServer
{ 
    public  int PacketId => 0x00;

    public void Read(IMinecraftStream stream)
    {
        
    }

    public void Write(IMinecraftStream stream)
    {
        
    }
}