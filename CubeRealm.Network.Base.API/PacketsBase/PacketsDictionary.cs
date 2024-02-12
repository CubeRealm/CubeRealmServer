namespace CubeRealm.Network.Base.API.PacketsBase;

public interface IPacketsDictionary
{
    IList<IPacket> GetByConnectionState(ConnectionState state);
}