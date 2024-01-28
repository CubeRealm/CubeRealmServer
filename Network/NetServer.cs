using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using NetworkAPI;

namespace Network;

public class NetServer : INetServer
{

    private ILogger<NetServer> Logger;
    
    private CancellationTokenSource CancellationToken { get; set; }
    private Socket Socket { get; set; }

    public NetServer(ILogger<NetServer> logger)
    {
        Logger = logger;
        CancellationToken = new CancellationTokenSource();
    }
    
    public void Start()
    {
        Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint end = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 25565);
        
        Socket.Bind(end);
        Socket.Listen(10);
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }
}