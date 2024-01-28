using System.Net.Sockets;
using Microsoft.Extensions.Logging;

namespace Network.Connection;

public class MinecraftNetConnection : NetConnection
{
    private protected override bool CompressionEnabled { get; }
    private protected override bool ConnectionState { get; }

    public MinecraftNetConnection(ILogger<NetConnection> logger, PacketFactory packetFactory, Socket socket) :
        base(logger, packetFactory, socket)
    {
        
    }
}