using CubeRealm.Network.Packets.Packets.Status.ToServer;
using NetworkAPI.Protocol;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Status.ToBoth;

public class Ping : Packet<StatusRequest>, IToServer, IToClient
{
    public byte ServerId => 0x01;
    public byte ClientId => 0x01;

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