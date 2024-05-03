using CubeRealm.Network.Base.API;
using World.API.Content;
using World.API.Content.Entity;

namespace World.API;

public interface IEntityFactory
{
    public Player CreatePlayer(INetConnection connection, Action<PlayerBuilder> builder);
}