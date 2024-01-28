using NetworkAPI.Protocol.Util;

namespace NetworkAPI.Protocol.Packets.Handshaking.ToServer;

public class Handshake : Packet<Handshake>, IToServer
{
    public byte ServerId => 0x00;
    
    public int Version { get; set; }
    public string Address { get; set; }
    public ushort Port { get; set; }
    public int NextState { get; set; }
    
    public override void Read(IMinecraftStream stream)
    {
        Version = stream.ReadVarInt();
        Address = stream.ReadString();
        Port = stream.ReadUShort();
        NextState = stream.ReadVarInt();
    }
    

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteVarInt(Version);
        stream.WriteString(Address);
        stream.WriteUShort(Port);
        stream.WriteVarInt(NextState);
    }
}