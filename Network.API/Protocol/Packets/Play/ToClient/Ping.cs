using NetworkAPI.Protocol.Util;

namespace NetworkAPI.Protocol.Packets.Play.ToClient;

public class Ping : Packet<Ping>, IToClient
{
    
    public int Id { get; set; }
    
    public override void Read(MinecraftStream stream)
    {
        Id = stream.ReadInt();
    }

    public override void Write(MinecraftStream stream)
    {
        stream.WriteInt(Id);
    }

    public byte ClientId => 0x30;
}