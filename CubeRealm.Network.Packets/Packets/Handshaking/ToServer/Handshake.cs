using NetworkAPI.Protocol;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Handshaking.ToServer;

public class Handshake : IPacket, IToServer
{
    public int PacketId => 0x00;
    
    public int ProtocolVersion { get; set; }
    public string Address { get; set; }
    public ushort Port { get; set; }
    public int NextState { get; set; }

    public void Read(IMinecraftStream stream)
    {
        ProtocolVersion = stream.ReadVarInt();
        Address = stream.ReadString();
        Port = stream.ReadUShort();
        NextState = stream.ReadVarInt();
    }
    

    public void Write(IMinecraftStream stream)
    {
        stream.WriteVarInt(ProtocolVersion);
        stream.WriteString(Address);
        stream.WriteUShort(Port);
        stream.WriteVarInt(NextState);
    }
}