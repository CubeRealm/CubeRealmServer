using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using NetworkAPI;

namespace Network;

public class NetServer : INetServer
{

    private ILogger<NetServer> Logger;
    
    private CancellationTokenSource CancellationToken { get; set; }
    private Socket ListenerSocket { get; set; }

    public NetServer(ILogger<NetServer> logger)
    {
        Logger = logger;
        CancellationToken = new CancellationTokenSource();
    }
    
    public void Start()
    {
        ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint end = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 25565);
        
        ListenerSocket.Bind(end);
        ListenerSocket.Listen(10);
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }
}