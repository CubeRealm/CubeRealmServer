using ConfigurationAPI.Configurations;

namespace ConfigurationAPI;

public interface IConfigLoader
{

    IServerConfig ServerConfig { get; }
    IWorldConfig WorldConfig { get; }
    IMobsSpawnConfig MobsSpawnConfig { get; }

}