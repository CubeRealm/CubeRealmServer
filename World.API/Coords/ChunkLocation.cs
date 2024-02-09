namespace World.API.Coords;

public class ChunkLocation
{
    
    public ChunkLocation(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public int x { get; set; }
    public int z { get; set; }

    public static ChunkLocation operator +(ChunkLocation location1, ChunkLocation location2)
    {
        return new ChunkLocation(location1.x + location2.x, location1.z + location2.z);
    }
    
    public static ChunkLocation operator -(ChunkLocation location1, ChunkLocation location2)
    {
        return new ChunkLocation(location1.x - location2.x, location1.z - location2.z);
    }
    
}