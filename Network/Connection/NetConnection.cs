using System.Net.Sockets;
using CubeRealm.Network.Packets;
using Ionic.Zlib;
using Microsoft.Extensions.Logging;
using NetworkAPI;
using NetworkAPI.Protocol;

namespace Network.Connection;

public abstract class NetConnection : INetConnection
{
    public bool IsConnected { get; private set; }
    
    private protected abstract bool CompressionEnabled { get; }
    private protected abstract bool ConnectionState { get; }
    private protected abstract int Version { get; }
    
    private ILogger<NetConnection> Logger { get; }
    private Socket Socket { get; }
    private CancellationTokenSource CancellationToken { get; }
    private Task WriteStream { get; }
    private Task ReadStream { get; }
    private PacketFactory PacketFactory { get; }
    
    internal NetConnection(ILogger<NetConnection> logger, PacketFactory packetFactory, Socket socket)
    {
        Logger = logger;
        Socket = socket;

        CancellationToken = new CancellationTokenSource();

        WriteStream = new Task(WriteToStream);
        ReadStream = new Task(ReadFromStream);

        PacketFactory = packetFactory;
        
        socket.Blocking = true;
    }

    internal void Start()
    {
        WriteStream.Start();
        ReadStream.Start();
    }
    
    public void UnsafeDisconnect()
    {
        CancellationToken.Cancel();
        Socket.Shutdown(SocketShutdown.Both);
        Socket.Close();
        Socket.Dispose();
        IsConnected = true;
    }

    private void WriteToStream()
    {
        
    }
    
    private void ReadFromStream()
    {
        try
        {
            using (NetworkStream networkStream = new NetworkStream(Socket))
            {
                using (MinecraftStream minecraftStream = new MinecraftStream(networkStream))
                {
                    while (!CancellationToken.IsCancellationRequested)
                    {
                        Packet packet;
                        int packetId;
                        byte[] packetData;
                        if (!CompressionEnabled)
                        {
                            var length = minecraftStream.ReadVarInt();
                            packetId = minecraftStream.ReadVarInt(out var packetIdLength);
                            if (length - packetIdLength > 0)
                            {
                                packetData = minecraftStream.Read(length - packetIdLength);
                            }
                            else
                            {
                                packetData = new byte[0];
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
                        packet = PacketFactory.GetToServer<Packet>(packetId, Version);
                        if (packet == null)
                        {
                            Logger.LogWarning($"Unhandled package! 0x{packetId:x2}");
                            continue;
                        }
                        Logger.LogInformation($" << Receiving packet 0x{packet.PacketId:x2} ({packet.GetType().Name})");
                        packet.Read(new MinecraftStream(new MemoryStream(packetData)));
                        HandlePacket(packet);
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

    private protected abstract void HandlePacket(Packet packet);

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