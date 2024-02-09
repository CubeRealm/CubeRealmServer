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

    public int IntX
    {
        get
        {
            return Convert.ToInt32(x);
        }
    }
    
    public int IntY
    {
        get
        {
            return Convert.ToInt32(y);
        }
    }
    
    public int IntZ
    {
        get
        {
            return Convert.ToInt32(z);
        }
    }
    
    public ChunkLocation ChunkLocation
    {
        get
        {
            return new ChunkLocation(Convert.ToInt32(x / 16), Convert.ToInt32(z / 16));
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