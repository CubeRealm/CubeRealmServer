using System.Net;
using System.Net.Sockets;
using CubeRealmServer.API;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetworkAPI;

namespace Network;

public class NetServer : INetServer
{

    private ILogger<NetServer> Logger;
    private CancellationTokenSource CancellationToken { get; set; }
    private Socket Socket { get; set; }
    private IOptions<ServerSettings> Options { get; }

    public NetServer(ILogger<NetServer> logger, IOptions<ServerSettings> options)
    {
        Logger = logger;
        CancellationToken = new CancellationTokenSource();
        Options = options;

        Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }
    
    public void Start()
    {
        IPEndPoint end = new IPEndPoint(IPAddress.Parse(Options.Value.NetServer.Address), Options.Value.NetServer.Port);
        
        Socket.Bind(end);
        Socket.Listen(10);
    }

    public void Stop()
    {
        
    }
}