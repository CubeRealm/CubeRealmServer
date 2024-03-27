using CubeRealm.Network.Base.API.PacketsBase;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Base.API.Packets.Login.ToClient;

public class EncryptionRequest : Packet<EncryptionRequest>, IToClient
{
    public override int PacketId => 0x01;
    
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
}