using NetworkAPI.Protocol.Util;

namespace NetworkAPI.Protocol.Packets.Login.ToClient;

public class EncryptionBegin : Packet<EncryptionBegin>, IToClient
{
    
    public string ServerId { get; set; }
    public byte[] PublicKey { get; set; }
    public byte[] VerifyToken { get; set; }
    
    public override void Read(IMinecraftStream stream)
    {
        ServerId = stream.ReadString();
        int publicKeyLength = stream.ReadVarInt();
        PublicKey = stream.Read(publicKeyLength);
        int verifyTokenLength = stream.ReadVarInt();
        VerifyToken = stream.Read(verifyTokenLength);
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteString(ServerId);
        stream.WriteVarInt(PublicKey.Length);
        stream.Write(PublicKey);
        stream.WriteVarInt(VerifyToken.Length);
        stream.Write(VerifyToken);
    }

    public byte ClientId => 0x01;
}