using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Network;
using Network.Connection;
using NetworkAPI;

namespace CubeRealm.Network.Base;

public class NetServer : INetServer
{
    private ILogger<NetServer> Logger { get; }
    private CancellationTokenSource CancellationToken { get; set; }
    private Socket ServerSocket { get; }
    private IOptions<ServerSettings> Options { get; }
    private NetConnectionFactory ConnectionFactory { get; }
    private List<NetConnection> Connections { get; }

    public NetServer(ILogger<NetServer> logger, IOptions<ServerSettings> options, NetConnectionFactory connectionFactory)
    {
        Logger = logger;
        CancellationToken = new CancellationTokenSource();
        Options = options;
        ConnectionFactory = connectionFactory;

        ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }
    
    public void Start()
    {
        string address = Options.Value.NetServer.Address;
        ushort port = Options.Value.NetServer.Port;
        IPEndPoint end = new IPEndPoint(IPAddress.Parse(address), port);
        
        ServerSocket.Bind(end);
        ServerSocket.Listen(10);
        
        Logger.LogInformation("Listen on {}:{}", address, port);

        ServerSocket.BeginAccept(IncomingConnection, null);
    }

    public void Stop()
    {
        ServerSocket.Close();
        ServerSocket.Dispose();
    }

    private async void IncomingConnection(IAsyncResult ar)
    {
        Socket? client = null;
        try
        {
            client = ServerSocket.EndAccept(ar);
        }
        catch
        {
            Logger.LogWarning("Error while connection accept");
        }

        ServerSocket.BeginAccept(IncomingConnection, null);
        if (client == null)
            return;

        NetConnection connection = ConnectionFactory.Create(client);
        await connection.Start();
    }
}