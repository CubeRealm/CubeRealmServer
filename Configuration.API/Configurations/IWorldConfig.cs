using ConfigurationAPI.Enums;

namespace ConfigurationAPI.Configurations;

public interface IWorldConfig
{
    string WorldPrefix { get; }
    bool AllowNether { get; }
    bool AllowEnd { get; }
    byte ViewDistance { get; }
    byte SimulationDistance { get; }
    Difficulty Difficulty { get; }
    Gamemode Gamemode { get; }
    bool Hardcore { get; }
    bool Pvp { get; }

}