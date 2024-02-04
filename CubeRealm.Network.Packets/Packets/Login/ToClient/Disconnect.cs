using NetworkAPI.Protocol.Util;

namespace NetworkAPI.Protocol.Packets.Login.ToClient;

public class Disconnect : Packet<Disconnect>, IToClient
{
    
    public string Reason { get; set; }
    
    public override void Read(IMinecraftStream stream)
    {
        Reason = stream.ReadString();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteString(Reason);
    }

    public byte ClientId => 0x00;
}