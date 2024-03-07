using World.API.Block;
using World.API.Content;

namespace World.API.Item;

public interface IItem
{
    Material Type { get; set; }
    IBlock PlaceableBlock { get; set; }
    
}