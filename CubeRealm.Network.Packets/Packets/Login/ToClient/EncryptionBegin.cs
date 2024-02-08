using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Login.ToClient;

public class EncryptionBegin : IPacket, IToClient
{
    public int PacketId => 0x01;
    
    public string ServerId { get; set; }
    public byte[] PublicKey { get; set; }
    public byte[] VerifyToken { get; set; }


    public void Read(IMinecraftStream stream)
    {
        ServerId = stream.ReadString();
        int publicKeyLength = stream.ReadVarInt();
        PublicKey = stream.Read(publicKeyLength);
        int verifyTokenLength = stream.ReadVarInt();
        VerifyToken = stream.Read(verifyTokenLength);
    }

    public void Write(IMinecraftStream stream)
    {
        stream.WriteString(ServerId);
        stream.WriteVarInt(PublicKey.Length);
        stream.Write(PublicKey);
        stream.WriteVarInt(VerifyToken.Length);
        stream.Write(VerifyToken);
    }
}