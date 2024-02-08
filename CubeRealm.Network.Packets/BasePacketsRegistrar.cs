using CubeRealm.Network.Packets.Packets.Handshaking.ToServer;
using CubeRealm.Network.Packets.Packets.Status.ToServer;
using NetworkAPI.Protocol;
using Ping = CubeRealm.Network.Packets.Packets.Status.ToBoth.Ping;

namespace CubeRealm.Network.Packets;

public class PacketsRegistrar
{
    public Dictionary<int, Func<IPacket>> RegisterBaseToServer<T>() where T : IPacket, IToServer
    {
        return new Dictionary<int, Func<IPacket>>
        {
            { 0, () => new Handshake() },
            { 0, () => new StatusRequest() },
            { 1, () => new Ping() }
            
        };
    }
}