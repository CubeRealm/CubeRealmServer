using CubeRealm.Network.Packets.Packets.Status.ToServer;
using NetworkAPI.Protocol;
using NetworkAPI.Protocol.Packets.Handshaking.ToServer;
using Ping = CubeRealm.Network.Packets.Packets.Status.ToServer.Ping;

namespace CubeRealm.Network.Packets;

public class PacketsRegistrar
{
    public Dictionary<int, Func<Packet>> RegisterBaseToServer<T>() where T : Packet, IToServer
    {
        return new Dictionary<int, Func<Packet>>
        {
            { 0, () => new Handshake() },
            { 0, () => new StatusRequest() },
            { 1, () => new Ping() }
            
        };
    }
}