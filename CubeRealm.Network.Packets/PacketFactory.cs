using CubeRealm.Network.Packets.Packets.Handshaking.ToServer;
using CubeRealm.Network.Packets.Packets.Status.ToBoth;
using CubeRealm.Network.Packets.Packets.Status.ToClient;
using CubeRealm.Network.Packets.Packets.Status.ToServer;
using Network;
using NetworkAPI;

namespace CubeRealm.Network.Packets;

public class PacketFactory : IPacketFactory
{
    //<version, Packet>
    private IDictionary<int, PacketsDictionary> PacketsToServer { get; }
    private IDictionary<int, PacketsDictionary> PacketsToClient { get; }
    
    public PacketFactory()
    {
        PacketsDictionary toServer = new PacketsDictionary
        {
            Handshake = [typeof(Handshake)],
            Status = [typeof(StatusRequest), typeof(Ping)]
        };
        PacketsDictionary toClient = new PacketsDictionary
        {
            Status = [typeof(StatusResponse), typeof(Ping)]
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
    
    public IPacket GetToClient<T>(ConnectionState connectionState, int packetId, int version) where T : IPacket
    {
        Type packetType = PacketsToClient[version].GetByConnectionState(connectionState)[packetId];
        return (T)Activator.CreateInstance(packetType);
    }
    
    public IPacket GetToServer<T>(ConnectionState connectionState, int packetId, int version) where T : IPacket
    {
        Type packetType = PacketsToServer[version].GetByConnectionState(connectionState)[packetId];
        return (T)Activator.CreateInstance(packetType);
    }
}