using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Login.ToClient;

public class Disconnect : Packet<Disconnect>, IToClient
{
    public override int PacketId => 0x00;
    public string Reason { get; set; }
    
    public override void Read(IMinecraftStream stream)
    {
        Reason = stream.ReadString();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteString(Reason);
    }
}