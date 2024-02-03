using CubeRealm.Network.Packets.Packets.Status.ToServer;
using NetworkAPI.Protocol;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Status.ToClient;

public class StatusResponse : Packet<StatusRequest>, IToServer
{
    public byte ServerId => 0x00;

    public string JsonString { get; set; }
    
    public override void Read(IMinecraftStream stream)
    {
        JsonString = stream.ReadString();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteString(JsonString);
    }
}