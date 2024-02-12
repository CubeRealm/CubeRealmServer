using CubeRealm.Network.Base.API.PacketsBase;

namespace CubeRealm.Network.Version765;

public class ProtocolVersion : IProtocolVersion
{
    public int Version { get; }
    public IPacketsDictionary AllClientPackets { get; }
    public IPacketsDictionary AllServerPackets { get; }
}