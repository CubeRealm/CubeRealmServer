using Network;
using NetworkAPI;
using NetworkAPI.Protocol;

namespace CubeRealm.Network.Packets;

public class PacketFactory : IPacketFactory
{
    //<version, <packedId, Packet>>
    private IDictionary<int, PacketsDictionary> PacketsToServer { get; } = new Dictionary<int, PacketsDictionary>();
    private IDictionary<int, PacketsDictionary> PacketsToClient { get; } = new Dictionary<int, PacketsDictionary>();
    
    public PacketFactory()
    {
        
    }
    
    public Packet GetToClient<T>(ConnectionState connectionState, int packetId, int version) where T : Packet
    {

        return (T)PacketsToClient[version].GetByConnectionState(connectionState)[packetId]();
    }
    
    public Packet GetToServer<T>(ConnectionState connectionState, int packetId, int version) where T : Packet
    {
        return (T)PacketsToServer[version].GetByConnectionState(connectionState)[packetId]();
    }
}