using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using PluginAPI;

namespace CubeRealmServer;

class Program
{
    public static void Main(string[] args)
    {
        bool isDebug = args.Contains("--debug");
        List<Type> plugins = new();
        Action<IServiceCollection, Type> pluginsLoader = (collection, type) =>
        {
            collection.AddSingleton(type);
            plugins.Add(type);
        };
        ServiceProvider serviceProvider = new ServiceCollection()
            .AddLogging(builder => builder.AddSimpleConsole(options =>
            {
                options.ColorBehavior = LoggerColorBehavior.Enabled;
                options.IncludeScopes = true;
                options.TimestampFormat = "HH:mm:ss.fff ";
            }).SetMinimumLevel(isDebug ? LogLevel.Trace : LogLevel.Information))
            .AddFromDirectory(Path.Combine(isDebug ? "../../../../CubeRealmTesting/" : ".", "plugins"), pluginsLoader)
            .BuildServiceProvider();
    }
}