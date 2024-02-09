using World.API.Content;
using World.API.Coords;

namespace World.API.World;

public class Chunk
{
    
    public IBlock[,,] Blocks { get; set; }

    public void SetBlock(Location location, IBlock block)
    {
        Blocks[location.BlockX, location.BlockY, location.BlockZ] = block;
    }
    
}