using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Play.ToClient;

public class ServerData : Packet<ServerData>, IToClient
{
    public override int PacketId => 0x49;
    
    public string Motd { get; set; }
    public bool HasIcon { get; set; }
    public int Size { get; set; }
    public byte[] Icon { get; set; }
    public bool EnforceSecureChat { get; set; }
    
    public override void Read(IMinecraftStream stream)
    {
        Motd = stream.ReadString();
        HasIcon = stream.ReadBool();
        Size = stream.ReadVarInt();
        Icon = stream.ReadByteArray();
        EnforceSecureChat = stream.ReadBool();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteString(Motd);
        stream.WriteBool(HasIcon);
        stream.WriteVarInt(Size);
        stream.WriteByteArray(Icon);
        stream.WriteBool(EnforceSecureChat);
    }
}