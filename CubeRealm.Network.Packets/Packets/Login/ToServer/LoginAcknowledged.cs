using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Login.ToServer;

public class LoginAcknowledged : IPacket, IToServer
{
    public int PacketId => 0x03;
    
    public void Read(IMinecraftStream stream)
    {
        //No fields
    }

    public void Write(IMinecraftStream stream)
    {
        //No fields
    }
}