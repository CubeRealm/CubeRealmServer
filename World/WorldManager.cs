using World.API;

namespace World;

public class WorldManager : IWorldManager
{
    public List<MinecraftWorld> Worlds { get; }

    public MinecraftWorld? GetWorldByName(string name)
    {
        foreach (MinecraftWorld minecraftWorld in Worlds)
        {
            if (minecraftWorld.WorldName == name) return minecraftWorld;
        }
        return null;
    }

    public void LoadAll()
    {
        if (!Directory.Exists(WORLDS_FOLDER))
        {
            Directory.CreateDirectory(WORLDS_FOLDER);
        }

        string[] directories = Directory.GetDirectories(WORLDS_FOLDER);
        foreach (string directory in directories)
        {
            
        }
    }
    
    public async Task Load(MinecraftWorld world)
    {
        world.Load();
    }

    public void Load(string name)
    {
        throw new NotImplementedException();
    }

    public void SaveAll()
    {
        throw new NotImplementedException();
    }

    public void Save(string name)
    {
        throw new NotImplementedException();
    }

    public void Save(MinecraftWorld world)
    {
        throw new NotImplementedException();
    }

    private const string WORLDS_FOLDER = "Worlds/";
}