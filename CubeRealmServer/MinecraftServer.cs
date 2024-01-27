using CubeRealmServer.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Network;
using Plugin;
using PluginAPI;

namespace CubeRealmServer;

public class MinecraftServer : IHostedService, IMinecraftServer
{
    private IServiceProvider ServiceProvider { get; }

    private NetServer NetworkServer { get; }

    public MinecraftServer(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        
        NetworkServer = new NetServer();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        IPluginActivator pluginActivator = ServiceProvider.GetRequiredService<IPluginActivator>();
        ((PluginActivator) pluginActivator).Activate();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}