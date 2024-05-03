using System.Collections.Concurrent;
using System.Net.Sockets;
using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.API.PacketsBase;
using CubeRealm.Network.Base.PacketsBase.Packets;
using CubeRealm.Network.Base.PacketsBase.Packets.Status.ToBoth;
using CubeRealm.Network.Base.PacketsBase.Packets.Status.ToClient;
using CubeRealm.Network.Base.PacketsBase.Packets.Status.ToServer;
using CubeRealmServer.API;
using Ionic.Zlib;
using Microsoft.Extensions.Logging;
using NetworkAPI.Protocol.Util;
using CompressionLevel = Ionic.Zlib.CompressionLevel;
using CompressionMode = Ionic.Zlib.CompressionMode;

namespace CubeRealm.Network.Base.Connection;

internal class NetConnection : INetConnection
{
    private ConnectionState _connectionState;
    
    private BlockingCollection<IPacket> PacketsQueue { get; } = new ();
    
    private ILogger<NetConnection> Logger { get; }
    private Socket Socket { get; }
    private IMinecraftServer MinecraftServer { get; }
    private CancellationTokenSource CancellationToken { get; }
   
    private IPacketHandler PacketHandler { get; set; }
    private Task ReadStream { get; }
    private IPacketFactory PacketFactory { get; }
    
    public bool IsConnected { get; private set; }
    
    public bool CompressionEnabled { get; private set; }
    public IPacketCollector Collector { get; }

    public ConnectionState ConnectionState
    {
        get => _connectionState;
        internal set
        {
            _connectionState = value;
            NewConnectionState?.Invoke(this, _connectionState);
        }
    }

    public event EventHandler<ConnectionState>? NewConnectionState;
    
    internal NetConnection(
        ILogger<NetConnection> logger,
        IPacketFactory packetFactory, 
        IMinecraftServer server,
        NetworkFactory networkFactory,
        Socket socket)
    {
        Logger = logger;
        Socket = socket;

        CancellationToken = new CancellationTokenSource();

        Collector = networkFactory.CreateCollector(this);
        PacketHandler = networkFactory.CreateHandler(this);
        ReadStream = new Task(ReadFromStream);
        
        PacketFactory = packetFactory;
        MinecraftServer = server;
        
        ConnectionState = ConnectionState.Handshake;
        
        socket.Blocking = true;
    }

    internal void Start()
    {
        ReadStream.Start();
    }
    
    public void UnsafeDisconnect()
    {
        CancellationToken.Cancel();
        Socket.Shutdown(SocketShutdown.Both);
        Socket.Close();
        Socket.Dispose();
        IsConnected = false;
    }

    public void MinecraftStream(Action<IMinecraftStream> actionStream)
    {
        using (NetworkStream networkStream = new NetworkStream(Socket))
            using (MinecraftStream stream = new MinecraftStream(networkStream))
                actionStream(stream);
    }
    
    private void ReadFromStream()
    {
        Logger.LogTrace("Connection reader started {}", Thread.CurrentThread.Name);
        try
        {
            using (NetworkStream networkStream = new NetworkStream(Socket))
            {
                using (MinecraftStream minecraftStream = new MinecraftStream(networkStream))
                {
                    while (!CancellationToken.IsCancellationRequested)
                    {
                        IPacket packet;
                        int packetId;
                        byte[] packetData;
                        if (!CompressionEnabled)
                        {
                            var length = minecraftStream.ReadVarInt();
                            packetId = minecraftStream.ReadVarInt(out var packetIdLength);
                            //Logger.LogTrace($"packetId {packetId:x2}");
                            //Logger.LogTrace("length - packetIdLength = {}", length - packetIdLength);
                            if (length - packetIdLength > 0)
                            {
                                packetData = minecraftStream.Read(length - packetIdLength);
                            }
                            else
                            {
                                packetData = Array.Empty<byte>();
                            }
                        }
                        else
                        {
                            var packetLength = minecraftStream.ReadVarInt();
                            var dataLength = minecraftStream.ReadVarInt(out var br);
                            if (dataLength == 0)
                            {
                                packetId = minecraftStream.ReadVarInt(out var readMore);
                                packetData = minecraftStream.Read(packetLength - (br + readMore));
                            }
                            else
                            {
                                var data = minecraftStream.Read(packetLength - br);
                                DecompressData(data, out var decompressed);
                                using (var b = new MemoryStream(decompressed))
                                {
                                    using (var a = new MinecraftStream(b))
                                    {
                                        packetId = a.ReadVarInt(out var l);
                                        packetData = a.Read(dataLength - l);
                                    }
                                }
                            }
                        }
                        Logger.LogInformation($"Try read packet 0x{packetId:x2} (pre_handle)");
                        packet = PacketFactory.GetToServer<IPacket>(ConnectionState, packetId);
                        if (packet == null)
                        {
                            Logger.LogWarning($"Unhandled package! 0x{packetId:x2}");
                            continue;
                        }
                        Logger.LogInformation($" << Receiving packet 0x{packet.PacketId:x2} ({packet.GetType().Name})");
                        packet.Read(new MinecraftStream(new MemoryStream(packetData)));
                        PreHandlePacket(packet);
                    }
                }
            }
        }
        catch (Exception e)
        {
            if (e is OperationCanceledException)
                return;
            if (e is EndOfStreamException)
                return;
            if (e is IOException)
                return;
            Logger.LogError(e, "An unhandled exception occurred while processing network!");
        }
        finally
        {
            UnsafeDisconnect();
        }
    }

    private void PreHandlePacket(IPacket packet)
    {
        if (ConnectionState == ConnectionState.Handshake || ConnectionState == ConnectionState.Status)
        {
            if (packet is Handshake handshake)
            {
                Logger.LogTrace("Handshake {} {} {} {}",
                    handshake.ProtocolVersion,
                    handshake.Address,
                    handshake.Port,
                    handshake.NextState);
                if (handshake.NextState == 1)
                {
                    ConnectionState = ConnectionState.Status;
                }
                if (handshake.NextState == 2)
                {
                    ConnectionState = ConnectionState.Login;
                    PacketHandler.ChangeStateTo(ConnectionState);
                    Logger.LogDebug("Change state to login!");
                }
            }

            if (packet is StatusRequest)
            {
                PacketsQueue.Add(new StatusResponse
                {
                    JsonString = MinecraftServer.CachedStatus
                });
            }
            
            if (packet is Ping)
            {
                PacketsQueue.Add(new Ping());
            }
            return;
        }
        
        PacketHandler.HandlePacket(packet);
    }

    public static void DecompressData(byte[] inData, out byte[] outData)
    {
        using (var outMemoryStream = new MemoryStream())
        {
            using (var outZStream = new ZlibStream(outMemoryStream, CompressionMode.Decompress, CompressionLevel.Default, true))
            {
                outZStream.Write(inData, 0, inData.Length);
            }
            outData = outMemoryStream.ToArray();
        }
    }
}