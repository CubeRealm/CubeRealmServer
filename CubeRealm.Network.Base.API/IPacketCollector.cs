using CubeRealm.Network.Base.API.PacketsBase;

namespace CubeRealm.Network.Base.API;

public interface IPacketCollector
{
    public void AddToNext(IPacket packet);
}