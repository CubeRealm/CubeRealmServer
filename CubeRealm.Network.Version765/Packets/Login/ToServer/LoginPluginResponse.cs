using CubeRealm.Network.Base.API.PacketsBase;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Version765.Packets.Login.ToServer;

public class LoginPluginResponse : Packet<LoginPluginResponse>, IToServer
{
    public override int PacketId => 0x02;

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