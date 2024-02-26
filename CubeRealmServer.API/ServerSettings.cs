using CubeRealm.Config;
using CubeRealm.Network.Base.API;
using Microsoft.Extensions.Options;
using NetworkAPI;

namespace CubeRealmServer.API;

public class ServerSettings
{
 
    public GeneralSettings General { get; set; }
    public PluginsSettings Plugins { get; set; }
    public NetworkSettings NetServer { get; set; }
}