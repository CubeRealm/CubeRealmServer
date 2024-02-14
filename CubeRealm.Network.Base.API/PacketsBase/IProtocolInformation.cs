namespace CubeRealm.Network.Base.API.PacketsBase;

public interface IProtocolInformation
{
    int Version { get; }
    PacketsDictionary AllToClientPackets { get; }
    PacketsDictionary AllToServerPackets { get; }
    IPacketHandler CreatePacketHandler(IServiceProvider serviceProvider, Action<IPacket> sendPacket);
}