using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Login.ToClient;

public class SetCompression : Packet<SetCompression>, IToClient
{
    public override int PacketId => 0x03;
    public int Threshold { get; set; }
    
    public override void Read(IMinecraftStream stream)
    {
        Threshold = stream.ReadVarInt();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteVarInt(Threshold);
    }
}