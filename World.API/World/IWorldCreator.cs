using World.API.Coords;

namespace World.API.World;

public interface IWorldCreator
{

    Chunk GenerateChunk(ChunkLocation location);

}