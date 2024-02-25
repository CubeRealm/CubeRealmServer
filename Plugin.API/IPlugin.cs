namespace PluginAPI;

public interface IPlugin
{
    
    string Name { get; set; }
    string Version { get; set; }
    List<string> Dependencies { get; set; }
    
    void Load();
    void Enable();
}