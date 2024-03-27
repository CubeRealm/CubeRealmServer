using World.API;
using World.API.World;

namespace PluginAPI;

/// <summary>
///  Entry point of your plugin. You should to implement this
///  If you want to use Dependencies you need to get Main Class of plugin you wanted to use
/// </summary>
public interface IPlugin
{
    
    /// <summary>
    /// Name of plugin which you will see in console
    /// </summary>
    string Name { get; set; }
    
    /// <summary>
    /// Version of your plugin
    /// </summary>
    string Version { get; set; }
    
    /// <summary>
    /// Authors of the plugin
    /// </summary>
    string[] Authors { get; set; }
    
    /// <summary>
    /// Method Load executing after adding plugin in list
    /// </summary>
    void Load();
    
    /// <summary>
    /// Method Enable executing after all plugins has been added
    /// </summary>
    void Enable();
    IMinecraftWorld? DefaultWorld { get; }
}