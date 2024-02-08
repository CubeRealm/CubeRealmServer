using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Login.ToServer;

public class LoginPluginResponse : IPacket, IToServer
{
    public int PacketId => 0x02;

    public int MessageId { get; set; }

    public void Read(IMinecraftStream stream)
    {
        MessageId = stream.ReadVarInt();
    }

    public void Write(IMinecraftStream stream)
    {
        stream.WriteVarInt(MessageId);
    }
}