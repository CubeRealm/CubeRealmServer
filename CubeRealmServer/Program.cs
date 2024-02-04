using System.Reflection;
using CubeRealm.Network.Packets;
using CubeRealmServer.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Network;
using NetworkAPI;
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
                }).SetMinimumLevel(LogLevel.Trace))
            .UseContentRoot(context.ContentRoot)
            .ConfigureAppConfiguration(builder => 
                builder.AddJsonFile(configPath))
            .ConfigureServices((builderContext, collection) => 
                Configure(builderContext, collection.AddSingleton(context)))
            .UseConsoleLifetime()
            .Build();
        
        await host.RunAsync();
    }

    private static void Configure(HostBuilderContext ctx, IServiceCollection services)
    {
        IConfigurationSection section = ctx.Configuration.GetSection("Server");
        services.AddOptions<ServerSettings>().Bind(section);

        services
            .AddLogging()
            .AddSingleton<IPluginActivator, PluginActivator>()
            .AddSingleton<IMinecraftServer, MinecraftServer>()
            .AddSingleton<INetServer, NetServer>()
            .AddTransient<NetConnectionFactory>()
            .AddTransient<IPacketFactory, PacketFactory>();

        string pluginsPath = section.GetSection("Plugins").GetSection("Directory").Value!;
        List<Type> pluginTypes = new List<Type>();
        PluginActivator.AddFromDirectory(ref pluginTypes, pluginsPath);
        foreach (var type in pluginTypes)
            services.AddSingleton(type);
        
        services.AddHostedService<MinecraftServer>();
    }
}