using CubeRealmServer.API;

namespace CubeRealmServer;

public class HostEnv(string contentRoot) : IHostEnv
{
    public string ContentRoot => contentRoot;
}