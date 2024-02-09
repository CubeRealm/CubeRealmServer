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

        Location blockLocation = new Location(location.IntX % 16, location.IntY , location.IntZ % 16);
    }

    public void GenerateChunk(ChunkLocation location)
    {
        
    }


    public Chunk GetChunkByLocation(ChunkLocation location) { return Chunks[location.x, location.z]; } 
    public Chunk GetChunkByLocation(Location location) { return Chunks[location.ChunkLocation.x, location.ChunkLocation.z]; } 
    
}