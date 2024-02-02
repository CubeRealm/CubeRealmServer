using System.Collections.ObjectModel;
using NetworkAPI;
using NetworkAPI.Protocol;
using NetworkAPI.Protocol.Packets.Handshaking.ToServer;

namespace CubeRealm.Network.Packets;

public class PacketFactory : IPacketFactory
{
    //<version, <packedId, Packet>>
    private static IDictionary<int, IDictionary<int, Func<Packet>>> PacketsToServer { get; } = new Dictionary<int, IDictionary<int, Func<Packet>>>();
    private static IDictionary<int, IDictionary<int, Func<Packet>>> PacketsToClient { get; } = new Dictionary<int, IDictionary<int, Func<Packet>>>();
    
    static PacketFactory()
    {
        PacketsToServer.Add(0, new Dictionary<int, Func<Packet>>
        {
            
        });
    }
    
    public Packet GetToClient<T>(int packetId, int version) where T : Packet
    {
        return (T)PacketsToClient[version][packetId]();
    }
    
    public Packet GetToServer<T>(int packetId, int version) where T : Packet
    {
        return (T)PacketsToServer[version][packetId]();
    }
}