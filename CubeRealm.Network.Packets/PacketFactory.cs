using CubeRealm.Network.Packets.Packets.Handshaking.ToServer;
using CubeRealm.Network.Packets.Packets.Status.ToBoth;
using CubeRealm.Network.Packets.Packets.Status.ToClient;
using CubeRealm.Network.Packets.Packets.Status.ToServer;
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
        PacketsDictionary toServer = new PacketsDictionary
        {
            Handshake = new Dictionary<int, Func<Packet>>
            {
                { 0, () => new Handshake() }
            },
            Status = new Dictionary<int, Func<Packet>>
            {
                { 0, () => new StatusRequest() },
                { 1, () => new Ping() }
            }
        };
        PacketsDictionary toClient = new PacketsDictionary
        {
            Status = new Dictionary<int, Func<Packet>>
            {
                { 0, () => new StatusResponse() },
                { 1, () => new Ping() }
            }
        };
        
        PacketsToServer = new Dictionary<int, PacketsDictionary>
        {
            { 0, toServer },
            { 765, toServer }
        };
        
        PacketsToClient = new Dictionary<int, PacketsDictionary>
        {
            { 0, toClient },
            { 765, toClient }
        };
        
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