namespace World.API.Coords;

public readonly struct Location(double x, double y, double z)
{
    public double X { get; } = x;
    public double Y { get; } = y;
    public double Z { get; } = z;

    public int BlockX => Convert.ToInt32(X);
    public int BlockY => Convert.ToInt32(Y);
    public int BlockZ => Convert.ToInt32(Z);

    public ChunkLocation ChunkLocation => new(Convert.ToInt32(X / 16), Convert.ToInt32(Z / 16));

    public static Location operator +(Location location1, Location location2)
    {
        return new Location(location1.X + location2.X, location1.Y + location2.Y, location1.Z + location2.Z);
    }
    
    public static Location operator -(Location location1, Location location2)
    {
        return new Location(location1.X - location2.X, location1.Y - location2.Y, location1.Z - location2.Z);
    }
    
}