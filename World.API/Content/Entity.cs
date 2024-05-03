using World.API.Content.Entity;

namespace World.API.Content;

public class Entities<T> where T : class, IEntity<T>, new()
{
    public static Entities<Zombie> Zombie => new(typeof(Zombie), "zombie", 54);
    public static Entities<Player> Player => new(typeof(Player), "player", -1, false);

    public Type Class { get; }
    public string Name { get; }
    public int TypeId { get; }
    public bool Independent { get; }
    
    private Entities(Type itType, string name, int typeId, bool independent = true)
    {
        Class = itType;
        Name = name;
        TypeId = typeId;
        Independent = independent;
    }
}
