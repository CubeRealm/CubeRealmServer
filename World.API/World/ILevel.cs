using World.API.Block;
using World.API.Chunk;
using World.API.Coords;

namespace World.API.World;

/// <summary>
/// Interface of the game world
/// </summary>
public interface ILevel
{
    /// <summary>
    /// 2D array of chunks (x and z)
    /// </summary>
    IChunk[,] Chunks { get; }
    
    /// <summary>
    /// Placing block in location
    /// </summary>
    /// <param name="block"></param>
    /// <param name="location"></param>
    void SetBlock(IBlock block, Location location);
    
    /// <summary>
    /// Removing (replacing by AIR) in location
    /// </summary>
    /// <param name="location"></param>
    void RemoveBlock(Location location);
}