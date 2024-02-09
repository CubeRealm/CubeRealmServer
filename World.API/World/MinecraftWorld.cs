using World.API.Content;
using World.API.Coords;

namespace World.API.World;

public class MinecraftWorld
{
    
    public IWorldCreator WorldCreator { get; set; }
    public Chunk[,] Chunks { get; set; }

    public void SetBlock(Location location, IBlock block)
    {
        ChunkLocation chunkLocation = location.ChunkLocation;
        Chunk chunk = GetChunkByLocation(chunkLocation);

        Location blockLocation = new Location(location.BlockX % 16, location.BlockY , location.BlockZ % 16);
    }

    public void GenerateChunk(ChunkLocation location)
    {
        
    }


    public Chunk GetChunkByLocation(ChunkLocation location) { return Chunks[location.X, location.Z]; } 
    public Chunk GetChunkByLocation(Location location) { return Chunks[location.ChunkLocation.X, location.ChunkLocation.Z]; } 
    
}