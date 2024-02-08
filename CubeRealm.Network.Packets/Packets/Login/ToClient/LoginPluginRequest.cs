using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Login.ToClient;

public class LoginPluginRequest : IPacket, IToClient
{
    public int PacketId => 0x04;

    public int MessageId { get; set; }
    public string Channel { get; set; }
    public byte[] Data { get; set; }

    public void Read(IMinecraftStream stream)
    {
        MessageId = stream.ReadVarInt();
        Channel = stream.ReadString();
        Data = stream.ReadBuffer();
    }

    public void Write(IMinecraftStream stream)
    {
        stream.WriteVarInt(MessageId);
        stream.WriteString(Channel);
        stream.WriteBuffer(Data);
    }
}
