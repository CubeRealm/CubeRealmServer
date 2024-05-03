using CubeRealm.Network.Base.API;
using World.API;
using World.API.Content;
using World.API.Content.Entity;

namespace World;

public class EntityFactory : IEntityFactory
{
    public Player CreatePlayer(INetConnection connection, Action<PlayerBuilder> builder)
    {
        {
            return new Player
            {
                Connection = connection,
                Id = Guid.NewGuid(),
                Type = Entities<Player>.Player
            };
        }
    }
}