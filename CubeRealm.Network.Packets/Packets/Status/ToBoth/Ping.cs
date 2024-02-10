using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Status.ToBoth;

public class Ping : Packet<Ping>, IToServer, IToClient
{
    public override int PacketId => 0x01;

    public long Time { get; private set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();

    public override void Read(IMinecraftStream stream)
    {
        Time = stream.ReadLong();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteLong(Time);
    }
   
}