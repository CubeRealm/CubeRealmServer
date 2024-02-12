using CubeRealm.Network.Base.API.PacketsBase;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Version765.Packets.Login.ToClient;

public class LoginPluginRequest : Packet<LoginPluginRequest>, IToClient
{
    public override int PacketId => 0x04;

    public int MessageId { get; set; }
    public string Channel { get; set; }
    public byte[] Data { get; set; }

    public override void Read(IMinecraftStream stream)
    {
        MessageId = stream.ReadVarInt();
        Channel = stream.ReadString();
        Data = stream.ReadBuffer();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteVarInt(MessageId);
        stream.WriteString(Channel);
        stream.WriteBuffer(Data);
    }
}
