using NetworkAPI.Protocol;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Status.ToServer;

public class Ping : Packet<StatusRequest>, IToServer
{
    public byte ServerId => 0x01;

    public long ClientTime { get; private set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();

    public override void Read(IMinecraftStream stream)
    {
        ClientTime = stream.ReadVarLong();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteVarLong(ClientTime);
    }
}