using CubeRealm.Network.Base.API.PacketsBase;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Version765.Packets.Configuration.ToClient;

public class FinishConfiguration : Packet<FinishConfiguration>, IToClient
{
    public override int PacketId => 0x02;
    
    public override void Read(IMinecraftStream stream)
    {
        
    }

    public override void Write(IMinecraftStream stream)
    {
        
    }
}