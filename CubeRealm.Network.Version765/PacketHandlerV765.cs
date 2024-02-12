using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.API.PacketsBase;
using Microsoft.Extensions.Logging;
using ConnectionState = System.Data.ConnectionState;

namespace CubeRealm.Network.Version765;

public class PacketHandlerV765(ILogger<PacketHandlerV765> logger, IPacketFactory packetFactory, Action<IPacket> packetSender) : 
    PacketHandler(logger, packetFactory, packetSender)
{
    public override event EventHandler<ConnectionState>? NewState;

    public override void HandlePacket(IPacket packet)   
    {
        
    }

    public override void StartLogin()
    {
        
    }
}