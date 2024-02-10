using CubeRealm.Network.Base.Connection;
using CubeRealm.Network.Base.PacketsBase;
using CubeRealm.Network.Packets;
using Microsoft.Extensions.Logging;
using ConnectionState = System.Data.ConnectionState;

namespace CubeRealm.Network.Version765;

public class PacketHandlerV765(ILogger<PacketHandlerV765> logger, PacketFactory packetFactory, Action<IPacket> packetSender) : 
    PacketHandler(logger, packetFactory, packetSender)
{
    protected override event EventHandler<ConnectionState>? NewState;

    protected override void HandlePacket(IPacket packet)   
    {
        
    }

    protected override void StartLogin()
    {
        
    }
}