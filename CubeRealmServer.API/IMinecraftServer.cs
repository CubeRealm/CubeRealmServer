using CubeRealm.Network.Base.API;
using CubeRealm.Network.Base.PacketsBase;
using Microsoft.Extensions.Options;
using NetworkAPI;
using PluginAPI;

namespace CubeRealmServer.API;

public interface IMinecraftServer
{
    INetServer Network { get; }
    IPluginActivator PluginActivator { get; }
    IOptions<ServerSettings> Options { get; }
    
    Motd Status { get; }
    string CachedStatus { get; }

    //Adds object for 1 tick
    public void AddForTick(ITickable tickable);
    
    //Adds an object for the entire lifetime of the running server
    public void AddForTicking(ITickable tickable, CancellationToken cancellationToken);
}