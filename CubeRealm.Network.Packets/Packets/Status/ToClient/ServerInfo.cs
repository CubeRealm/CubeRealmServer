using NetworkAPI.Protocol.Util;

namespace NetworkAPI.Protocol.Packets.Status.ToClient;

public class ServerInfo : Packet<ServerInfo>, IToClient
{
    public byte ClientId => 0x00;
    
    
    
    public string Response { get; set; }
    
    public override void Read(IMinecraftStream stream)
    {
        Response = stream.ReadString();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteString(Response);
    }
}