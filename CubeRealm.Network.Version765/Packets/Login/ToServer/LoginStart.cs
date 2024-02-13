using CubeRealm.Network.Base.API.PacketsBase;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Version765.Packets.Login.ToServer;

public class LoginStart : Packet<LoginStart>, IToServer
{
    public override int PacketId => 0x00;
    
    public string Name { get; private set; }
    public Guid PlayerUUID { get; private set; }
    
    public override void Read(IMinecraftStream stream)
    {
        Name = stream.ReadString();
        PlayerUUID = stream.ReadUuid();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteString(Name);
        stream.WriteUuid(PlayerUUID);
    }
}