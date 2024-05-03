using CubeRealm.Network.Base.API.PacketsBase;
using NetworkAPI.Protocol.Util;

namespace CubeRealm.Network.Base.PacketsBase.Packets.Status.ToClient;

public class BundleDelimiter : Packet<BundleDelimiter>, IToClient
{
    public override int PacketId => 0x00;

    public override void Read(IMinecraftStream stream) {}
    public override void Write(IMinecraftStream stream) {}
}