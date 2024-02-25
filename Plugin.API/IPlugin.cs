namespace PluginAPI;

public interface IPlugin
{
    
    string Name { get; set; }
    string Version { get; set; }
    string[] Authors { get; set; }
    
    void Load();
    void Enable();
}