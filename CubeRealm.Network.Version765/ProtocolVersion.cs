using CubeRealm.Network.Base.API.PacketsBase;
using CubeRealm.Network.Version765.Packets.Configuration.ToClient;
using CubeRealm.Network.Version765.Packets.Login.ToClient;
using CubeRealm.Network.Version765.Packets.Login.ToServer;
using EncryptionBegin = CubeRealm.Network.Version765.Packets.Login.ToClient.EncryptionBegin;

namespace CubeRealm.Network.Version765;

public class ProtocolVersion : IProtocolVersion
{
    public int Version => 765;

    public PacketsDictionary AllToClientPackets { get; } = new()
    {
        Login = [new Disconnect(), new EncryptionBegin(), new LoginPluginRequest(), new SetCompression()],
        Configuration = [new FinishConfiguration()]
    };

    public PacketsDictionary AllToServerPackets { get; } = new()
    {
        Login = [new Packets.Login.ToServer.EncryptionBegin(), new LoginAcknowledged(), new LoginPluginResponse()],
        Configuration = [new FinishConfiguration()]
    };
}