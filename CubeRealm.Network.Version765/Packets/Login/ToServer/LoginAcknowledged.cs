using CubeRealm.Network.Packets;
using NetworkAPI.Protocol.Util;

namespace CubeRealmProtocol.Version765.Login.ToServer;

public class LoginAcknowledged : Packet<LoginAcknowledged>, IToServer
{
    public override int PacketId => 0x03;
    
    public override void Read(IMinecraftStream stream)
    {
        //No fields
    }

    public override void Write(IMinecraftStream stream)
    {
        //No fields
    }
}