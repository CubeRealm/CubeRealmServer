using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.API.PacketsBase;
using CubeRealmServer.API;

namespace CubeRealm.Network.Base.PacketsBase;

public class PacketHandlerFactory(IServiceProvider serviceProvider)
{
    private IServiceProvider ServiceProvider { get; } = serviceProvider;

    internal IPacketHandler Create(int version, Action<IPacket> packetSender)
    {
        List<Type> types = ModulesLoader.FromFile<IPacketHandler>("CubeRealm.Network.Version765.dll");
        return (IPacketHandler)Activator.CreateInstance(types[0], ServiceProvider, packetSender);
    }
}