using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.API.PacketsBase;
using CubeRealm.Network.Version765;
using CubeRealmServer.API;

namespace CubeRealm.Network.Base.PacketsBase;

public class PacketHandlerFactory(IServiceProvider serviceProvider)
{
    private IServiceProvider ServiceProvider { get; } = serviceProvider;

    private List<Type> Types { get; } = ModulesLoader.FromFile<IPacketHandler>("CubeRealm.Network.Version765.dll");
    
    internal IPacketHandler Create(int version, Action<IPacket> packetSender)
    {
        return (IPacketHandler)Activator.CreateInstance(Types[0], ServiceProvider, packetSender);
    }
}