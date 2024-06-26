using CubeRealm.Network.Base.API.PacketsBase;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Base.PacketsBase.Packets.Status.ToClient;

public class StatusResponse : Packet<StatusResponse>, IToClient
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