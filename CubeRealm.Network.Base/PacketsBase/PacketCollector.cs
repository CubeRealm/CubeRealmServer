using System.Collections.Concurrent;
using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.API.PacketsBase;
using CubeRealm.Network.Base.Connection;
using CubeRealm.Network.Base.PacketsBase.Packets.Status.ToClient;
using CubeRealmServer.API;
using Microsoft.Extensions.Logging;
using NetworkAPI.Protocol.Util;
using World.API;
using ConnectionState = CubeRealm.Network.Base.API.ConnectionState;

namespace CubeRealm.Network.Base.PacketsBase;

internal class PacketCollector : IPacketCollector, ITickable, IDisposable
{
    private readonly ILogger<PacketCollector> _logger;
    
    private readonly ConcurrentQueue<IPacket> _packets = new();
    private readonly CancellationTokenSource _tickDisabler;
    private readonly IWorlds _worlds;
    private readonly NetConnection _connection;

    private bool HasNext => !_packets.IsEmpty;

    public PacketCollector(
        ILogger<PacketCollector> logger,
        IWorlds worlds,
        IMinecraftServer server,
        NetConnection connection)
    {
        _logger = logger;
        _tickDisabler = new CancellationTokenSource();
        server.AddForTicking(this, _tickDisabler.Token);
        _worlds = worlds;
        _connection = connection;
    }
    
    private IPacket Next()
    {
        if (_packets.TryDequeue(out var peeked))
            return peeked;
        throw new Exception("Next element not found!");
    }

    private void Write(IPacket packet, IMinecraftStream stream)
    {
        _logger.LogTrace(">> Sending packet {}", packet.GetType().Name);
        MinecraftStream fakeMcStream = new MinecraftStream(new MemoryStream());
        fakeMcStream.WriteVarInt(packet.PacketId);
        packet.Write(fakeMcStream);
        int packetLen = (int)fakeMcStream.Length;
                    
        stream.WriteVarInt(packetLen);
        stream.Write(((MemoryStream) fakeMcStream.BaseStream).ToArray());
    }
    
    public void AddToNext(IPacket packet)
    {
        _packets.Enqueue(packet);
    }
   
    public async Task Tick()
    {
        if (_connection.ConnectionState is not ConnectionState.Play)
            return;
        
        //TODO Packet collecting
        
        _connection.MinecraftStream(stream =>
        {
            Write(new BundleDelimiter(), stream);
            int i = 0;
            while (HasNext)
            {
                if (i == 4096)
                    break;
                IPacket packet = Next();
                Write(packet, stream);
                i++;
            }
            Write(new BundleDelimiter(), stream);
        });
    }

    public void Dispose()
    {
        _tickDisabler.Cancel();
        _tickDisabler.Dispose();
    }
}