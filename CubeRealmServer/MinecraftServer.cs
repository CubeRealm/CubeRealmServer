using CubeRealm.Network.Base;
using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.PacketsBase;
using CubeRealmServer.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Network;
using NetworkAPI;
using Newtonsoft.Json;
using Plugin;
using PluginAPI;

namespace CubeRealmServer;

public class MinecraftServer : IHostedService, IMinecraftServer
{
    private Motd _motd;
    
    private ILogger<MinecraftServer> Logger { get; }
    private IServiceProvider ServiceProvider { get; }
    public INetServer Network { get; private set; }
    public IPluginActivator PluginActivator { get; private set; }

    public Motd Status
    {
        get => _motd;
        private set => CachedStatus = JsonConvert.SerializeObject(_motd = value);
    }

    public string CachedStatus { get; private set; }

    public MinecraftServer(ILogger<MinecraftServer> logger, IServiceProvider serviceProvider)
    {
        Logger = logger;
        ServiceProvider = serviceProvider;
        
        Status = new()
        {
            Version = new Motd.VersionPart
            {
                Name = "1.20.4",
                Protocol = 765
            },
            Players = new Motd.PlayersPart
            {
                Max = int.MaxValue,
                Online = int.MaxValue
            },
            Description = new Motd.DescriptionPart
            {
                Text = "MDK"
            },
            Icon = "",
            PreviewsChat = false,
            SecureChat = false
        };
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    { 
        Logger.LogInformation("Server start thread '{}'", Thread.CurrentThread.Name);
        
        Logger.LogInformation("Activating plugins");
        PluginActivator? pluginActivator = null;
        
        
        await Task.Run(() =>
        {
            PluginActivator = pluginActivator = ServiceProvider.GetOriginalService<IPluginActivator, PluginActivator>();
            
            pluginActivator.Activate();
            pluginActivator.Action("Plugin {} loaded", plugin => plugin.Load());
            Logger.LogInformation("Activated");
        }, cancellationToken);

        Logger.LogInformation("Starting NetServer");
        
        
        await Task.Run(() =>
        {
            PacketFactory packetFactory = ServiceProvider.GetOriginalService<IPacketFactory, PacketFactory>();
            packetFactory.AddPackets();
            
            ServiceProvider.GetRequiredService<PacketHandlerFactory>();
            
            NetServer netServer;
            Network = netServer = ServiceProvider.GetOriginalService<INetServer, NetServer>();
            
            netServer.Start();
            Logger.LogInformation("Started NetServer");
        }, cancellationToken);

        
        Task.WaitAll();
        
        Logger.LogInformation("Enabling plugins");
        pluginActivator!.Action("Plugin {} enabled", plugin => plugin.Enable());
        Logger.LogInformation("Enabled plugins");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}