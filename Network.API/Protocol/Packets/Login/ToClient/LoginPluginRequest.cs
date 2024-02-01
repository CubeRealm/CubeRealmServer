using NetworkAPI.Protocol;
using NetworkAPI.Protocol.Util;

namespace NetworkAPI.Protocol.Packets.Login.ToClient;
public class LoginPluginRequest : Packet<LoginPluginRequest>, IToClient
{
    public byte ClientId => 0x04;

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
