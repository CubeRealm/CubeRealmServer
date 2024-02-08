using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Login.ToClient;

public class Disconnect : IPacket, IToClient
{
    public int PacketId => 0x00;
    public string Reason { get; set; }
    
    public void Read(IMinecraftStream stream)
    {
        Reason = stream.ReadString();
    }

    public void Write(IMinecraftStream stream)
    {
        stream.WriteString(Reason);
    }
}