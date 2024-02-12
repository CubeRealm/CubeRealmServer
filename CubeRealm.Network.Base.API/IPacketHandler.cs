using CubeRealm.Network.Base.API.PacketsBase;

namespace CubeRealm.Network.Base.API;

public interface IPacketHandler
{
    event EventHandler<ConnectionState>? NewState;
    void HandlePacket(IPacket packet);
    void ChangeStateTo(ConnectionState connectionState);
}