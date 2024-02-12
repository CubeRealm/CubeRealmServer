using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.API.PacketsBase;
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
        
    }

    public void ChangeStateTo(ConnectionState connectionState)
    {
        if (ConnectionState.Login == connectionState)
        {
            
        }
    }
}