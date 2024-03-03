using World.API.Chunk;
using World.API.Coords;

namespace World.API.World;

public interface ILevelGenerator
{
    long Seed { get; set; }
    
    IChunk GenerateChunkData(ref IChunk chunk);
}