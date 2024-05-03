using System.Net.Sockets;
using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.Connection;
using CubeRealm.Network.Base.PacketsBase;
using CubeRealmServer.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using World.API;

namespace CubeRealm.Network.Base;

public class NetworkFactory(IServiceProvider serviceProvider)
{
    private ILoggerFactory LoggerFactory => serviceProvider.GetRequiredService<ILoggerFactory>();
    private IPacketFactory PacketFactory => serviceProvider.GetRequiredService<IPacketFactory>();
    private IMinecraftServer Server => serviceProvider.GetRequiredService<IMinecraftServer>();
    private NetworkFactory NetFactory => serviceProvider.GetRequiredService<NetworkFactory>();
    private IWorlds Worlds => serviceProvider.GetRequiredService<IWorlds>();
    private IEntityFactory EntityFactory => serviceProvider.GetRequiredService<IEntityFactory>();
    
    internal NetConnection Create(Socket socket)
    {
        return new NetConnection(
            LoggerFactory.CreateLogger<NetConnection>(),
            PacketFactory, 
            Server, 
            NetFactory,
            socket);
    }

    internal IPacketCollector CreateCollector(NetConnection connection)
    {
        return new PacketCollector(LoggerFactory.CreateLogger<PacketCollector>(), Worlds, Server, connection);
    }
    
    internal IPacketHandler CreateHandler(NetConnection connection)
    {
        return new PacketHandler(LoggerFactory.CreateLogger<PacketHandler>(), connection, EntityFactory);
    }
}