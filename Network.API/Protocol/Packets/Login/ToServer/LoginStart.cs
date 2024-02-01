using NetworkAPI.Protocol.Util;

namespace NetworkAPI.Protocol.Packets.Login.ToServer;

public class LoginStart : Packet<LoginStart>, IToServer
{
    
    public string Name { get; set; }
    public Guid Uuid { get; set; }
    
    public override void Read(IMinecraftStream stream)
    {
        Name = stream.ReadString();
        Uuid = stream.ReadUuid();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteString(Name);
        stream.WriteUuid(Uuid);
    }

    public byte ServerId => 0x00;
}