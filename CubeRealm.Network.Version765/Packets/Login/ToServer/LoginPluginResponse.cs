using CubeRealm.Network.Base.API.PacketsBase;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Version765.Packets.Login.ToServer;

public class LoginPluginResponse : Packet<LoginPluginResponse>, IToServer
{
    public override int PacketId => 0x02;

    public int MessageId { get; set; }
    public bool Successful { get; set; }
    public byte[] Data { get; set; }

    public override void Read(IMinecraftStream stream)
    {
        MessageId = stream.ReadVarInt();
        Successful = stream.ReadBool();
        Data = stream.ReadBuffer();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteVarInt(MessageId);
        stream.WriteBool(Successful);
        stream.WriteBuffer(Data);
    }
}