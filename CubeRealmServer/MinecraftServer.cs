using CubeRealm.Network.Base;
using CubeRealmServer.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Network;
using NetworkAPI;
using Plugin;
using PluginAPI;

namespace CubeRealmServer;

public class MinecraftServer(ILogger<MinecraftServer> logger, IServiceProvider serviceProvider)
    : IHostedService, IMinecraftServer
{
    private ILogger<MinecraftServer> Logger => logger;
    private IServiceProvider ServiceProvider => serviceProvider;
    public INetServer Network { get; private set; }
    public IPluginActivator PluginActivator { get; private set; }

    public async Task StartAsync(CancellationToken cancellationToken)
    { 
        Logger.LogInformation("Server start thread '{}'", Thread.CurrentThread.Name);
        
        Logger.LogInformation("Activating plugins");
        PluginActivator? pluginActivator = null;
        
        
        await Task.Run(() =>
        {
            PluginActivator = pluginActivator = ServiceProvider.GetOriginalService<IPluginActivator, PluginActivator>();
            
            pluginActivator.Activate();
            Logger.LogInformation("Activated");
        }, cancellationToken);

        Logger.LogInformation("Starting NetServer");
        
        
        await Task.Run(() =>
        {
            NetServer netServer;
            Network = netServer = ServiceProvider.GetOriginalService<INetServer, NetServer>();
            
            netServer.Start();
            Logger.LogInformation("Started NetServer");
        }, cancellationToken);

        
        Task.WaitAll();
        
        Logger.LogInformation("Enabling plugins");
        pluginActivator!.Enable();
        Logger.LogInformation("Enabled plugins");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}