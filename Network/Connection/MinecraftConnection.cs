using System.Net.Sockets;
using CubeRealm.Network.Packets;
using Microsoft.Extensions.Logging;

namespace Network.Connection;

public class MinecraftConnection : NetConnection
{
    private protected override bool CompressionEnabled { get; }
    private protected override bool ConnectionState { get; }

    public MinecraftConnection(ILogger<NetConnection> logger, PacketFactory packetFactory, Socket socket) :
        base(logger, packetFactory, socket)
    {
        
    }
}