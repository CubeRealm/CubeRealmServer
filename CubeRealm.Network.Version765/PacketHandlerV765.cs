using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.API.PacketsBase;
using CubeRealm.Network.Version765.Packets.Configuration.ToClient;
using CubeRealm.Network.Version765.Packets.Login.ToClient;
using CubeRealm.Network.Version765.Packets.Login.ToServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CubeRealm.Network.Version765;

public class PacketHandlerV765(IServiceProvider serviceProvider, Action<IPacket> sendPacket) : IPacketHandler
{
    private ILogger<PacketHandlerV765> Logger { get; } = serviceProvider.GetRequiredService<ILogger<PacketHandlerV765>>();
    private IPacketFactory PacketFactory { get; } = serviceProvider.GetRequiredService<IPacketFactory>();
    
    public event EventHandler<ConnectionState>? NewState;

    public void HandlePacket(IPacket packet)   
    {
        Logger.LogDebug("Handle packet {}", packet.GetType().Name);
        if (packet is LoginStart loginStart)
        {
            Logger.LogInformation("Start login player {}", loginStart.Name);
            sendPacket(new LoginSuccess
            {
                Username = loginStart.Name,
                UUID = loginStart.PlayerUUID
            });
        }
        if (packet is LoginAcknowledged loginAcknowledged)
        {
            Logger.LogInformation("Configure player");
            NewState?.Invoke(this, ConnectionState.Configuration);
            sendPacket(new FinishConfiguration());
        }
        if (packet is LoginPluginResponse loginPluginRequest)
        {
            sendPacket(new LoginPluginRequest()
            {
                MessageId = 0,
                Channel = "minecraft",
                Data = loginPluginRequest.Data
            });
        }
        if (packet is FinishConfiguration configuration)
        {
            Logger.LogInformation("Configure player end");
            NewState?.Invoke(this, ConnectionState.Play);
        }
    }

    public void ChangeStateTo(ConnectionState connectionState)
    {
        Logger.LogDebug("Change state to {}", connectionState);
        if (ConnectionState.Login == connectionState)
        {
            
        }
    }
}