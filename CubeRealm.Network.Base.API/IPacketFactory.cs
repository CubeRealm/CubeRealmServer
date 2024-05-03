using CubeRealm.Network.Base.API.PacketsBase;

namespace CubeRealm.Network.Base.API;

public interface IPacketFactory
{
    public T GetToClient<T>(ConnectionState connectionState, int packetId) where T : IPacket;
    public T GetToServer<T>(ConnectionState connectionState, int packetId) where T : IPacket;
}