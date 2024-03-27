using World.API.Content;
using World.API.Coords;

namespace World.API.Block.Implementation;

public class SimpleBlock(Material type, Location location) : IBlock
{
    public Material Type { get; set; } = type;
    public Location Location { get; set; } = location;
}