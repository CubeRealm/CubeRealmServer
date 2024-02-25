using CubeRealm.Network.Version765.Data;
using World.API.Coords;

namespace World.API.Block;

public interface IBlock
{

    Material Type { get; set; }
    Location Location { get; set; }

}