using System.Reflection;
using CubeRealm.Config;
using CubeRealmServer.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PluginAPI;

namespace Plugin;

public class PluginActivator : IPluginActivator
{
    private ILogger<PluginActivator> Logger { get; }
    private IServiceProvider Provider { get; }
    private List<Type> PluginsTypes { get; }
    private List<IPlugin> Plugins { get; }

    public PluginActivator(ILogger<PluginActivator> logger, IServiceProvider serviceProvider, IHostEnv hostEnv, IOptions<ServerSettings> config)
    {
        Logger = logger;
        Provider = serviceProvider;
        PluginsTypes = new();
        Plugins = new();

        if (!Directory.Exists(config.Value.Plugins.Directory))
            Directory.CreateDirectory(config.Value.Plugins.Directory);
        
        AddFromDirectory(Path.Combine(hostEnv.ContentRoot, config.Value.Plugins.Directory));
    }
    
    private void AddFromDirectory(string directory)
    {
        foreach (string file in Directory.GetFiles(directory, "*.dll"))
            AddFromFile(file);
    }
    
    private void AddFromFile(string fileName)
    {
        Assembly asm = Assembly.LoadFrom(fileName);
        
        foreach (Type unknownType in asm.GetExportedTypes())
        {
            if (unknownType.IsAssignableTo(typeof(IPlugin)))
            {
                PluginsTypes.Add(unknownType);
            }
        }
    }
    
    public void Activate()
    {
        foreach (var pluginType in PluginsTypes)
        {
            try
            {
                object? obj = Provider.GetService(pluginType);
                if (obj is IPlugin plugin)
                    Plugins.Add(plugin);
                else
                    throw new Exception("Plugin not found");
            }
            catch (Exception e)
            {
                Logger.LogError("Plugin activate exception: {}", e);       
            }
        }
    }

    public void Enable()
    {
        foreach (var plugin in Plugins)
        {
            try
            {
                plugin.Enable();
            }
            catch (Exception e)
            {
                Logger.LogError("Plugin enable exception: {}", e);
            }
        }
    }
}