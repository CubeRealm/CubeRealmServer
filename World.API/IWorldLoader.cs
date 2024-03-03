using World.API.World;

namespace World.API;

public interface IWorldLoader
{

    void Save(ILevel level);

    ILevel? Load(string name);

}