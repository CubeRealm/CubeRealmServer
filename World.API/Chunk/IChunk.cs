using World.API.Block;
using World.API.Coords;
using World.API.Item;

namespace World.API.Chunk;

public interface IChunk
{
    
    IBlock[,,] ChunkData { get; set; }
    ChunkLocation Location { get; set; }

    void SetBlock(IBlock block, Location location);
    void SetBlock(IItem block, Location location);
    void RemoveBlock(Location location);

}