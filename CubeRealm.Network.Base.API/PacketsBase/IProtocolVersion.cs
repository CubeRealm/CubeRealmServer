namespace CubeRealm.Network.Base.API.PacketsBase;

public interface IProtocolVersion
{
    int Version { get; }
    IPacketsDictionary AllClientPackets { get; }
    IPacketsDictionary AllServerPackets { get; }
}