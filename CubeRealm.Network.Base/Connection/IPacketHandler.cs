using System.Data;
using CubeRealm.Network.Packets;
using Microsoft.Extensions.Logging;
using NetworkAPI;

namespace CubeRealm.Network.Base.Connection;

public abstract class PacketHandler(ILogger<PacketHandler> logger, IPacketFactory packetFactory, Action<IPacket> packetSender)
{
    protected internal abstract event EventHandler<ConnectionState>? NewState;
    protected internal abstract void HandlePacket(IPacket packet);
    protected internal abstract void StartLogin();
}