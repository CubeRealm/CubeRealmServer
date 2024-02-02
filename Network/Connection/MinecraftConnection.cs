using System.Net.Sockets;
using CubeRealm.Network.Packets;
using Microsoft.Extensions.Logging;
using NetworkAPI.Protocol;

namespace Network.Connection;

public class MinecraftConnection : NetConnection
{
    private protected override bool CompressionEnabled { get; }
    private protected override bool ConnectionState { get; }
    private protected override int Version { get; }

    public MinecraftConnection(ILogger<NetConnection> logger, PacketFactory packetFactory, Socket socket) :
        base(logger, packetFactory, socket)
    {
        
    }

    private protected override void HandlePacket(Packet packet)
    {
        throw new NotImplementedException();
    }
}