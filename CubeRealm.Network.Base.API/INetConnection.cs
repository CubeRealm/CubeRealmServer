namespace CubeRealm.Network.Base.API;

public interface INetConnection
{
    bool CompressionEnabled { get; }
    ConnectionState ConnectionState { get; }

    bool IsConnected { get; }
    void UnsafeDisconnect();
}