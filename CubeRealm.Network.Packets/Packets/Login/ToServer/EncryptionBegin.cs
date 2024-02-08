using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Login.ToServer;
public class EncryptionBegin : IPacket, IToServer
{
    public int PacketId => 0x01;

    public byte[] SharedSecret { get; set; }
    public byte[] VerifyToken { get; set; }

    public void Read(IMinecraftStream stream)
    {
        int sharedSecretLength = stream.ReadVarInt();
        SharedSecret = stream.Read(sharedSecretLength);
        int verifyTokenLength = stream.ReadVarInt();
        VerifyToken = stream.Read(verifyTokenLength);
    }

    public void Write(IMinecraftStream stream)
    {
        stream.WriteVarInt(SharedSecret.Length);
        stream.Write(SharedSecret);
        stream.WriteVarInt(VerifyToken.Length);
        stream.Write(VerifyToken);
    }
}