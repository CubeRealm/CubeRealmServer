using ConfigurationAPI.Configurations;
using Newtonsoft.Json;

namespace Configuration.Configurations;

public class MobsSpawnConfig : IMobsSpawnConfig
{
    
    [JsonProperty("spawn_animals")] public bool SpawnAnimals { get; }
    [JsonProperty("spawn_monsters")] public bool SpawnMonsters { get; }
    
}