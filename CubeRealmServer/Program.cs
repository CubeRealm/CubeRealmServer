using Configuration;
using ConfigurationAPI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using PluginAPI;

namespace CubeRealmServer;

class Program
{
    public static void Main(List<string> args)
    {
        int debugIndex = args.IndexOf("--debug");
        string workDirectory = ".";

        if (debugIndex != -1)
            workDirectory = args[debugIndex + 1];
        
        string pluginsPath = Path.Combine(workDirectory, "plugins");
        string configPath = Path.Combine(workDirectory, "settings");
        
        List<Type> plugins = new();

        void PluginsLoader(IServiceCollection collection, Type type)
        {
            collection.AddScoped(type);
            plugins.Add(type);
        }

        ServiceProvider serviceProvider = new ServiceCollection()
            .AddLogging(builder => builder.AddSimpleConsole(options =>
            {
                options.ColorBehavior = LoggerColorBehavior.Enabled;
                options.IncludeScopes = true;
                options.TimestampFormat = "HH:mm:ss.fff ";
            }).SetMinimumLevel(debugIndex != -1 ? LogLevel.Trace : LogLevel.Information))
            //Configuration
            .AddSingleton<IConfigLoader, ConfigLoader>()
            .AddFromDirectory(pluginsPath, PluginsLoader)
            .BuildServiceProvider();

        IServiceProvider scopedServiceProvider = serviceProvider.CreateScope().ServiceProvider;
        ConfigLoader configLoader = (ConfigLoader)scopedServiceProvider.GetRequiredService<IConfigLoader>();
        configLoader.SaveDefaults(configPath);
        configLoader.LoadConfigs(configPath);
    }
}