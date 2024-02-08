using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Status.ToBoth;

public class Ping : IPacket, IToServer, IToClient
{
    public int PacketId => 0x01;

    public long Time { get; private set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();

    public void Read(IMinecraftStream stream)
    {
        Time = stream.ReadLong();
    }

    public void Write(IMinecraftStream stream)
    {
        stream.WriteLong(Time);
    }
   
}