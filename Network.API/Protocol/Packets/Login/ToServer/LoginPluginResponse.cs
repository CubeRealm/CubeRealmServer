using NetworkAPI.Protocol.Util;

namespace NetworkAPI.Protocol.Packets.Login.ToServer;

public class LoginPluginResponse : Packet<LoginPluginResponse>, IToServer
{
    public byte ServerId => 0x02;

    public int MessageId { get; set; }

    public override void Read(IMinecraftStream stream)
    {
        MessageId = stream.ReadVarInt();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteVarInt(MessageId);
    }
}