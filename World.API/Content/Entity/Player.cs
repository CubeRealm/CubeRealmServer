using CubeRealm.Network.Base.API;

namespace World.API.Content.Entity;

public class Player : ILivingEntity<Player>
{
    public Entities<Player> Type { get; init; }
    public Guid Id { get; init; }

    public INetConnection Connection { get; init; }
}