using World.API.Block;
using World.API.Coords;
using World.API.Item;

namespace World.API.Chunk;

/// <summary>
/// Chunk interface
/// </summary>
public interface IChunk
{
    
    /// <summary>
    /// 3D array of blocks in chunk
    /// </summary>
    IBlock[,,] ChunkData { get; set; }
    
    /// <summary>
    /// Location of chunk in the world
    /// </summary>
    ChunkLocation Location { get; set; }
    void SetBlock(IBlock block, Location location);
    void SetBlock(IItem block, Location location);
    void RemoveBlock(Location location);

}