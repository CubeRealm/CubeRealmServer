using System.Reflection;
using CubeRealm.Config;
using CubeRealmServer.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Plugin;
using PluginAPI;

namespace CubeRealmServer;

class Program
{
    public static async Task Main(string[] args)
    {
        IHostEnv context = new HostEnv(AppContext.BaseDirectory);
        
        Assembly assembly = Assembly.GetExecutingAssembly();
        string resourceName = "CubeRealmServer.appsettings.json";
        string configPath = Path.Combine(context.ContentRoot, "appsettings.json");
        if (!File.Exists(configPath))
            await using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                    await File.WriteAllTextAsync(configPath, await reader.ReadToEndAsync());
                    
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(builder => builder.AddSimpleConsole(options =>
                {
                    options.ColorBehavior = LoggerColorBehavior.Enabled;
                    options.IncludeScopes = true;
                    options.TimestampFormat = "HH:mm:ss.fff ";
                }))
            .UseContentRoot(context.ContentRoot)
            .ConfigureAppConfiguration(builder => 
                builder.AddJsonFile(configPath))
            .ConfigureServices((builderContext, collection) => 
                Configure(builderContext, collection.AddSingleton(context)))
            .UseConsoleLifetime()
            .Build();
        
        await host.StartAsync();
    }

    private static void Configure(HostBuilderContext ctx, IServiceCollection services)
    {
        var section = ctx.Configuration.GetSection("Server");
        services.AddOptions<ServerSettings>().Bind(section);
        
        services.AddLogging()
            //Plugins
            .AddSingleton<IPluginActivator, PluginActivator>()
            .AddSingleton<IMinecraftServer, MinecraftServer>()
            .AddHostedService<MinecraftServer>();
    }
}