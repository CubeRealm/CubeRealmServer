using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.API.PacketsBase;
using CubeRealm.Network.Version765.Packets.Configuration.ToClient;
using CubeRealm.Network.Version765.Packets.Login.ToClient;
using CubeRealm.Network.Version765.Packets.Login.ToServer;

namespace CubeRealm.Network.Version765;

public class ProtocolInformation : IProtocolInformation
{
    public int Version => 765;

    public PacketsDictionary AllToClientPackets { get; } = new()
    {
        Login = [new Disconnect(), new EncryptionRequest(), new LoginPluginRequest(), new SetCompression()],
        Configuration = [new FinishConfiguration()]
    };

    public PacketsDictionary AllToServerPackets { get; } = new()
    {
        Login = [new LoginStart(), new EncryptionResponse(), new LoginPluginResponse(), new LoginAcknowledged()],
        Configuration = [new FinishConfiguration()]
    };
    
    public IPacketHandler CreatePacketHandler(IServiceProvider serviceProvider, Action<IPacket> sendPacket)
    {
        return new PacketHandlerV765(serviceProvider, sendPacket);
    }
}