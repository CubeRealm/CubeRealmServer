using World.API.Chunk;
using World.API.Coords;

namespace World.API.World;

public interface ILevelGenerator
{
    long Seed { get; set; }
    
    IChunk GenerateChunkData(ref IChunk chunk);
    
    /// <summary>
    /// Generates chunk in specific location
    /// </summary>
    /// <param name="location">Location of the chunk</param>
    void GenerateChunk(ChunkLocation location);
}