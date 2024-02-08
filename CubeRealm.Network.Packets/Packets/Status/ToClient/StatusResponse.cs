using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Status.ToClient;

public class StatusResponse : IPacket, IToServer
{
    public int PacketId => 0x00;

    public string? JsonString { get; set; }
    
    public void Read(IMinecraftStream stream)
    {
        JsonString = stream.ReadString();
    }

    public void Write(IMinecraftStream stream)
    {
        stream.WriteString(JsonString);
    }
}