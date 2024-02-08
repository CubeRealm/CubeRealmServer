using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Play.ToClient;

public class Ping : IPacket, IToClient
{
    public int PacketId => 0x04;
    
    public int Id { get; set; }
    
    public void Read(IMinecraftStream stream)
    {
        Id = stream.ReadInt();
    }

    public void Write(IMinecraftStream stream)
    {
        stream.WriteInt(Id);
    }
}