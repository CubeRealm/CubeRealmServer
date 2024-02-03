using NetworkAPI.Protocol.Util;

namespace NetworkAPI.Protocol.Packets.Login.ToClient;

public class SetCompression : Packet<SetCompression>, IToClient
{
    
    public int Threshold { get; set; }
    
    public override void Read(IMinecraftStream stream)
    {
        Threshold = stream.ReadVarInt();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteVarInt(Threshold);
    }

    public byte ClientId => 0x03;
}