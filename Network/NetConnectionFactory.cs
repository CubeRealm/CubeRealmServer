using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Network.Connection;
using NetworkAPI;

namespace Network;

public class NetConnectionFactory(ILoggerFactory loggerFactory, IPacketFactory packetFactory, IServiceProvider provider)
{
    internal NetConnection Create(Socket socket)
    {
        return new MinecraftNetConnection(loggerFactory.CreateLogger<NetConnection>(), (PacketFactory)packetFactory, socket);
    }
}