using ConfigurationAPI.Configurations;
using Newtonsoft.Json;

namespace Configuration.Configurations;

public struct ServerConfig : IServerConfig
{
    [JsonProperty("host")] public string Host { get; }
    [JsonProperty("port")] public ushort Port { get; }
    [JsonProperty("max_players")] public int MaxPlayers { get; }
    [JsonProperty("motd")] public string Motd { get; }
    [JsonProperty("whitelist")] public bool Whitelist { get; }
}