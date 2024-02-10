using CubeRealm.Network.Packets;

namespace CubeRealm.Network.Base.PacketsBase;

public interface IProtocolVersion
{
    int Version { get; }
    PacketsDictionary AllClientPackets { get; }
    PacketsDictionary AllServerPackets { get; }
}