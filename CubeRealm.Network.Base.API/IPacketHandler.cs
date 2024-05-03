using CubeRealm.Network.Base.API.PacketsBase;

namespace CubeRealm.Network.Base.API;

public interface IPacketHandler
{
    void HandlePacket(IPacket packet);
    void ChangeStateTo(ConnectionState connectionState);
}