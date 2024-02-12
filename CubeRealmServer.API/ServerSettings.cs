using CubeRealm.Config;
using CubeRealm.Network.Base.API;
using NetworkAPI;

namespace CubeRealmServer.API;

public class ServerSettings
{
    
    public PluginsSettings Plugins { get; set; }
    public NetworkSettings NetServer { get; set; }
}