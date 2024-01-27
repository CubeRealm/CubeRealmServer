using ConfigurationAPI.Configurations;
using ConfigurationAPI.Enums;
using Newtonsoft.Json;

namespace Configuration.Configurations;

public class WorldConfig : IWorldConfig
{
    
    [JsonProperty("world_prefix")] public string WorldPrefix { get; }
    [JsonProperty("allow_nether")] public bool AllowNether { get; }
    [JsonProperty("allow_end")] public bool AllowEnd { get; }
    [JsonProperty("view_distance")] public byte ViewDistance { get; }
    [JsonProperty("simulation_distance")] public byte SimulationDistance { get; }
    [JsonProperty("difficulty")] public Difficulty Difficulty { get; }
    [JsonProperty("gamemode")] public Gamemode Gamemode { get; }
    [JsonProperty("hardcore")] public bool Hardcore { get; }
    [JsonProperty("pvp")] public bool Pvp { get; }
    
}