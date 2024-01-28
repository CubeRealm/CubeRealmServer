using System.Net;

namespace NetworkAPI;

public class NetworkSettings
{
    public required IPAddress Address { get; set; }
    public ushort Port { get; set; }
}