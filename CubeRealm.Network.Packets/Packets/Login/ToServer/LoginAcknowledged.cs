using NetworkAPI.Protocol.Util;

namespace NetworkAPI.Protocol.Packets.Login.ToServer;

public class LoginAcknowledged : Packet<LoginAcknowledged>, IToServer
{
    public override void Read(IMinecraftStream stream)
    {
        //No fields
    }

    public override void Write(IMinecraftStream stream)
    {
        //No fields
    }

    public byte ServerId => 0x03;
}