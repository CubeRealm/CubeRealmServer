using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Status.ToClient;

public class StatusResponse : Packet<StatusResponse>, IToServer
{
    public override int PacketId => 0x00;

    public string? JsonString { get; set; }
    
    public override void Read(IMinecraftStream stream)
    {
        JsonString = stream.ReadString();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteString(JsonString);
    }
}