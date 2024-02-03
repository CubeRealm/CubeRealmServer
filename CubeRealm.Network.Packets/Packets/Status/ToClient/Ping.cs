using NetworkAPI.Protocol;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Packets.Packets.Status.ToClient;

public class Ping : Packet<Ping>, IToClient
{
    
    public long Id { get; set; }
    
    public override void Read(IMinecraftStream stream)
    {
        Id = stream.ReadVarLong();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteVarLong(Id);
    }

    public byte ClientId => 0x04;
}