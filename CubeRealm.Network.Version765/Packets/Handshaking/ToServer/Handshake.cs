using CubeRealm.Network.Packets;
using NetworkAPI.Protocol.Util;

namespace CubeRealmProtocol.Version765.Handshaking.ToServer;

public class Handshake : Packet<Handshake>, IToServer
{
    public override int PacketId => 0x00;
    
    public int ProtocolVersion { get; set; }
    public string Address { get; set; }
    public ushort Port { get; set; }
    public int NextState { get; set; }

    public override void Read(IMinecraftStream stream)
    {
        ProtocolVersion = stream.ReadVarInt();
        Address = stream.ReadString();
        Port = stream.ReadUShort();
        NextState = stream.ReadVarInt();
    }
    

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteVarInt(ProtocolVersion);
        stream.WriteString(Address);
        stream.WriteUShort(Port);
        stream.WriteVarInt(NextState);
    }
}