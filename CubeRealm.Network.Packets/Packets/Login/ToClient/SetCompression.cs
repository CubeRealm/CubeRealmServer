using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Login.ToClient;

public class SetCompression : IPacket, IToClient
{
    public int PacketId => 0x03;
    public int Threshold { get; set; }
    
    public void Read(IMinecraftStream stream)
    {
        Threshold = stream.ReadVarInt();
    }

    public void Write(IMinecraftStream stream)
    {
        stream.WriteVarInt(Threshold);
    }
}