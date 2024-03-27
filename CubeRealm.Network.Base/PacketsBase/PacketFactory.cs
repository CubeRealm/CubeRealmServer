using System.Reflection;
using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.API.Packets.Configuration.ToBoth;
using CubeRealm.Network.Base.API.Packets.Login.ToClient;
using CubeRealm.Network.Base.API.Packets.Login.ToServer;
using CubeRealm.Network.Base.API.PacketsBase;
using CubeRealm.Network.Base.PacketsBase.Packets;
using CubeRealm.Network.Base.PacketsBase.Packets.Status.ToBoth;
using CubeRealm.Network.Base.PacketsBase.Packets.Status.ToClient;
using CubeRealm.Network.Base.PacketsBase.Packets.Status.ToServer;

namespace CubeRealm.Network.Base.PacketsBase;

public class PacketFactory : IPacketFactory
{
    private readonly Dictionary<IPacket, PropertyInfo[]> _types = new();
    
    //<version, Packet>
    private PacketsDictionary PacketsToServer { get; }
    private PacketsDictionary PacketsToClient { get; }
    
    public PacketFactory()
    {
        PacketsToServer = new PacketsDictionary
        {
            Handshake = [new Handshake()],
            Status = [new StatusRequest(), new Ping()],
            Login = [new Disconnect(), new EncryptionRequest(), new LoginPluginRequest(), new SetCompression()],
            Configuration = [new FinishConfiguration()],
            Play = []
        };
        PacketsToClient = new PacketsDictionary
        {
            Status = [new StatusResponse(), new Ping()],
            Login = [new LoginStart(), new EncryptionResponse(), new LoginPluginResponse(), new LoginAcknowledged()],
            Configuration = [new FinishConfiguration()],
            Play = []
        };
        
    }
    
    public T GetToClient<T>(ConnectionState connectionState, int packetId) where T : IPacket
    {
        IPacket packetType = PacketsToClient.GetByConnectionState(connectionState)[packetId];
        return (T)packetType.CreateNew();
    }
    
    public T GetToServer<T>(ConnectionState connectionState, int packetId) where T : IPacket
    {
        IPacket packetType = PacketsToServer.GetByConnectionState(connectionState)[packetId];
        return (T)packetType.CreateNew();
    }
    
    public T? GetToServerDeserialize<T>(ConnectionState connectionState, int packetId, MinecraftStream stream) where T : IPacket
    {
        throw new NotImplementedException();
        T packet = (T) PacketsToServer.GetByConnectionState(connectionState)[packetId];
        PropertyInfo[] properties;
        if (_types.TryGetValue(packet, out var propertyInfos))
        {
            properties = propertyInfos;
        }
        else
        {
            Type packetType = packet.GetType();
            properties = packetType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            _types.Add(packet, properties);
        }

        T newPacket = (T)packet.CreateNew();
        
        foreach (var propertyInfo in properties)
        {
            MethodInfo? setter = propertyInfo.SetMethod;
            if (setter == null)
                continue;
            
        }
        
        return newPacket;
    }
}