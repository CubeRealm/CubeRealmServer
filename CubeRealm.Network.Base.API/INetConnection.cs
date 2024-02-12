namespace CubeRealm.Network.Base.API;

public interface INetConnection
{
    bool IsConnected { get; }
    void UnsafeDisconnect();
}