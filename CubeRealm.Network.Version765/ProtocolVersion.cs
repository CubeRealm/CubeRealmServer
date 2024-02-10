using CubeRealm.Network.Base.PacketsBase;
using CubeRealm.Network.Packets;

namespace CubeRealm.Network.Version765;

public class ProtocolVersion : IProtocolVersion
{
    public int Version { get; }
    public PacketsDictionary AllClientPackets { get; }
    public PacketsDictionary AllServerPackets { get; }
}