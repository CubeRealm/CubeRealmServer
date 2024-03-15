namespace World.API;

public interface IWorldManager
{
    
    List<MinecraftWorld> Worlds { get; set; }
    MinecraftWorld? GetWorldByName(string name);
    
    void LoadAll();
    void Load(string name);

    void SaveAll();
    void Save(string name);
    void Save(MinecraftWorld world);

}