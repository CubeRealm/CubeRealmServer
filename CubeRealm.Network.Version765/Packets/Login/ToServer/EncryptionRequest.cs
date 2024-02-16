using CubeRealm.Network.Base.API.PacketsBase;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Version765.Packets.Login.ToServer;

public class EncryptionResponse : Packet<EncryptionResponse>, IToServer
{
    public override int PacketId => 0x01;

    public byte[] SharedSecret { get; set; }
    public byte[] VerifyToken { get; set; }

    public override void Read(IMinecraftStream stream)
    {
        int sharedSecretLength = stream.ReadVarInt();
        SharedSecret = stream.Read(sharedSecretLength);
        int verifyTokenLength = stream.ReadVarInt();
        VerifyToken = stream.Read(verifyTokenLength);
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteVarInt(SharedSecret.Length);
        stream.Write(SharedSecret);
        stream.WriteVarInt(VerifyToken.Length);
        stream.Write(VerifyToken);
    }
}