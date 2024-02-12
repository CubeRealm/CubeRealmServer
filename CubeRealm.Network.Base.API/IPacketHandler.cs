using CubeRealm.Network.Base.API.PacketsBase;
using Microsoft.Extensions.Logging;

namespace CubeRealm.Network.Base.API;

public abstract class PacketHandler(ILogger<PacketHandler> logger, IPacketFactory packetFactory, Action<IPacket> packetSender)
{
    public abstract event EventHandler<System.Data.ConnectionState>? NewState;
    public abstract void HandlePacket(IPacket packet);
    public abstract void StartLogin();
}