using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.API.Packets.Configuration.ToBoth;
using CubeRealm.Network.Base.API.Packets.Login.ToClient;
using CubeRealm.Network.Base.API.Packets.Login.ToServer;
using CubeRealm.Network.Base.API.PacketsBase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CubeRealm.Network.Base.PacketsBase;

public class PacketHandler(IServiceProvider serviceProvider, Action<IPacket> sendPacket) : IPacketHandler
{
    private ILogger<PacketHandler> Logger { get; } = serviceProvider.GetRequiredService<ILogger<PacketHandler>>();
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