using System.Collections.ObjectModel;
using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.API.PacketsBase;
using CubeRealmServer.API;

namespace CubeRealm.Network.Base.PacketsBase;

public class NetServerPacketHandlerFactory(IServiceProvider serviceProvider)
{
    private IServiceProvider ServiceProvider { get; } = serviceProvider;
    
    private IDictionary<int, IProtocolInformation> Factories { get; } = NetServer.LoadFactories();
        
    internal IPacketHandler Create(int version, Action<IPacket> packetSender)
    {
        return Factories[version].CreatePacketHandler(ServiceProvider, packetSender);
    }
}