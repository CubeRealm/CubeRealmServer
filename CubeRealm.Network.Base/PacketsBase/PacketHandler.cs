using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.API.Packets.Configuration.ToBoth;
using CubeRealm.Network.Base.API.Packets.Login.ToClient;
using CubeRealm.Network.Base.API.Packets.Login.ToServer;
using CubeRealm.Network.Base.API.PacketsBase;
using CubeRealm.Network.Base.Connection;
using Microsoft.Extensions.Logging;
using World.API;
using World.API.Content.Entity;

namespace CubeRealm.Network.Base.PacketsBase;

internal class PacketHandler(ILogger<PacketHandler> logger, NetConnection connection, IEntityFactory entityFactory) : IPacketHandler
{
    private string Name;
    private Guid UUID;
    
    public void HandlePacket(IPacket packet)
    {
        logger.LogDebug("Handle packet {}", packet.GetType().Name);
        if (packet is LoginStart loginStart)
        {
            logger.LogInformation("Start login player {}", loginStart.Name);
            connection.Collector.AddToNext(new LoginSuccess
            {
                Username = Name = loginStart.Name,
                UUID = UUID = loginStart.PlayerUUID
            });
        }
        if (packet is LoginAcknowledged loginAcknowledged)
        {
            logger.LogInformation("Configure player");
            connection.ConnectionState = ConnectionState.Configuration;
            connection.Collector.AddToNext(new FinishConfiguration());
        }
        if (packet is LoginPluginResponse loginPluginRequest)
        {
            connection.Collector.AddToNext(new LoginPluginRequest()
            {
                MessageId = 0,
                Channel = "minecraft",
                Data = loginPluginRequest.Data
            });
        }
        if (packet is FinishConfiguration configuration)
        {
            logger.LogInformation("Configure player end");
            connection.ConnectionState = ConnectionState.Play;
            Player player = entityFactory.CreatePlayer(connection, builder => {}); //TODO Player spawn
        }
    }

    public void ChangeStateTo(ConnectionState connectionState)
    {
        logger.LogDebug("Change state to {}", connectionState);
        if (ConnectionState.Login == connectionState)
        {
            
        }
    }
}