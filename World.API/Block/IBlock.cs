using World.API.Coords;
using World.API.Data;

namespace World.API.Block;

public interface IBlock
{

    Material Type { get; set; }
    Location Location { get; set; }

}