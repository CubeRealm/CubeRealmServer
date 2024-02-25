using World.API.Block;
using World.API.Chunk;
using World.API.Coords;
using World.API.Item;

namespace World.API.World;

public interface IWorld
{
    
    IChunk[,] Chunks { get; set; }
    string WorldName { get; set; }


    void GenerateChunk(ChunkLocation location);
    void SetBlock(IBlock block, Location location);
    void SetBlock(IItem block, Location location);
    void RemoveBlock(Location location);

}