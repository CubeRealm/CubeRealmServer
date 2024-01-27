using Newtonsoft.Json;

namespace ConfigurationAPI.Configurations;

public interface IServerConfig
{
    
    string Host { get; }
    ushort Port { get; }
    int MaxPlayers { get; }
    string Motd { get; }
    bool Whitelist { get; }
    
}