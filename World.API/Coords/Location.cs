namespace World.API.Coords;

public class Location
{
    public Location(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public double x { get; set; }
    public double y { get; set; }
    public double z { get; set; }


    public ChunkLocation ChunkLocation
    {
        get
        {
            return new ChunkLocation(x / 16, z / 16);
        }
    } 

    public static Location operator +(Location location1, Location location2)
    {
        return new Location(location1.x + location2.x, location1.y + location2.y, location1.z + location2.z);
    }
    
    public static Location operator -(Location location1, Location location2)
    {
        return new Location(location1.x - location2.x, location1.y - location2.y, location1.z - location2.z);
    }
    
}