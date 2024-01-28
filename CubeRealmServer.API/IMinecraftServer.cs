using NetworkAPI;
using PluginAPI;

namespace CubeRealmServer.API;

public interface IMinecraftServer
{
    INetServer Network { get; }
    IPluginActivator PluginActivator { get; }
}