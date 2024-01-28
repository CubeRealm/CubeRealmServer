using System.Net;

namespace NetworkAPI;

public class NetworkSettings
{
    public required string Address { get; set; }
    public ushort Port { get; set; }
}