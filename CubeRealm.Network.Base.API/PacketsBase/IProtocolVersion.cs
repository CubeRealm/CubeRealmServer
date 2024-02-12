using CubeRealm.Network.Base.PacketsBase;

namespace CubeRealm.Network.Base.API.PacketsBase;

public interface IProtocolVersion
{
    int Version { get; }
    PacketsDictionary AllToClientPackets { get; }
    PacketsDictionary AllToServerPackets { get; }
}