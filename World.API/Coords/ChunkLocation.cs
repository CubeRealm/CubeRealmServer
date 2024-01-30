namespace World.API.Coords;

public class ChunkLocation
{
    
    public ChunkLocation(double x, double z)
    {
        this.x = x;
        this.z = z;
    }

    public double x { get; set; }
    public double z { get; set; }

    public static ChunkLocation operator +(ChunkLocation location1, ChunkLocation location2)
    {
        return new ChunkLocation(location1.x + location2.x, location1.z + location2.z);
    }
    
    public static ChunkLocation operator -(ChunkLocation location1, ChunkLocation location2)
    {
        return new ChunkLocation(location1.x - location2.x, location1.z - location2.z);
    }
    
}