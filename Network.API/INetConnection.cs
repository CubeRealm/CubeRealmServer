namespace NetworkAPI;

public interface INetConnection
{
    bool IsConnected { get; }
    void UnsafeDisconnect();
}