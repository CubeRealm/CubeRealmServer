using System.Net.Sockets;
using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.API.PacketsBase;
using CubeRealm.Network.Base.Connection;
using CubeRealm.Network.Base.PacketsBase;
using CubeRealmServer.API;
using Microsoft.Extensions.Logging;
using NetworkAPI;

namespace CubeRealm.Network.Base;

public class PacketHandlerFactory
{
    private ILoggerFactory LoggerFactory { get; }
    private IPacketFactory PacketFactory { get; }
    
    
    public PacketHandlerFactory(ILoggerFactory loggerFactory, IPacketFactory packetFactory)
    {
        LoggerFactory = loggerFactory;
        PacketFactory = packetFactory;
    }
    
    internal PacketHandler Create(int version, Action<IPacket> packetSender)
    {
        List<Type> types = ModulesLoader.FromFile<PacketHandler>("CubeRealm.Network.Version765.dll");
        return (PacketHandler)Activator.CreateInstance(types[0], LoggerFactory.CreateLogger<PacketHandler>(), PacketFactory, packetSender);
    }
}