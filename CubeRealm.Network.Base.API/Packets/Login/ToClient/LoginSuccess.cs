using CubeRealm.Network.Base.API.PacketsBase;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Base.API.Packets.Login.ToClient;

public class LoginSuccess : Packet<LoginSuccess>, IToServer
{
    public override int PacketId => 0x02;
    
    public Guid UUID { get; set; }
    public string Username { get; set; }
    public int NumberOfProperties { get; set; }
    
    public override void Read(IMinecraftStream stream)
    {
        UUID = stream.ReadUuid();
        Username = stream.ReadString();
        NumberOfProperties = stream.ReadVarInt();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteUuid(UUID);
        stream.WriteString(Username);
        stream.WriteVarInt(NumberOfProperties);
    }
}