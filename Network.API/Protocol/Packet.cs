using NetworkAPI.Protocol.Util;

namespace NetworkAPI.Protocol;

public abstract class Packet
{

    public int PacketId { get; set; } = -1;

    public abstract void Read(IMinecraftStream stream);
    public abstract void Write(IMinecraftStream stream);

}

public abstract class Packet<TPacket> : Packet where TPacket : Packet<TPacket>, new()
{
    public static TPacket CreateObject()
    {
        return new TPacket();
    }
}