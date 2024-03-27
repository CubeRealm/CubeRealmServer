using CubeRealm.Network.Base.API.PacketsBase;

namespace World.API.World;

public interface ILevelNetwork
{
    public Task HandlePackets(IPacket[] packet);
    public Task<IPacket[]> CollectPackets();
}