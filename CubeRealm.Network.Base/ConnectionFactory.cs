using System.Net.Sockets;
using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.Connection;
using CubeRealm.Network.Base.PacketsBase;
using CubeRealmServer.API;
using Microsoft.Extensions.Logging;

namespace CubeRealm.Network.Base;

public class ConnectionFactory(ILoggerFactory loggerFactory, IPacketFactory packetFactory, IServiceProvider serviceProvider, IMinecraftServer server)
{
    internal NetConnection Create(Socket socket)
    {
        return new NetConnection(loggerFactory.CreateLogger<NetConnection>(), serviceProvider, (PacketFactory)packetFactory, server, socket);
    }
}