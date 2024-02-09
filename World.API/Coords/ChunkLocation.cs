namespace World.API.Coords;

public readonly struct ChunkLocation(int x, int z)
{
    public int X { get; } = x;
    public int Z { get; } = z;

    public static ChunkLocation operator +(ChunkLocation location1, ChunkLocation location2)
    {
        return new ChunkLocation(location1.X + location2.X, location1.Z + location2.Z);
    }

    public static ChunkLocation operator -(ChunkLocation location1, ChunkLocation location2)
    {
        return new ChunkLocation(location1.X - location2.X, location1.Z - location2.Z);
    }
    
}