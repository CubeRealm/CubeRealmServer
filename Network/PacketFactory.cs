using NetworkAPI;
using NetworkAPI.Protocol;

namespace Network;

public class PacketFactory : IPacketFactory
{
   
    public T Get<T>(int connectionState, int packetId, int version) where T : Packet, new()
    {
        return new T();
    }
}