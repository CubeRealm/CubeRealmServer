using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CubeRealm.Network.Packets;

public class Motd
{
    [JsonProperty(PropertyName = "version")] public VersionPart Version { get; init; }
    [JsonProperty(PropertyName = "players")] public PlayersPart Players { get; init; }
    [JsonProperty(PropertyName = "description")] public DescriptionPart Description { get; init; }
    [JsonProperty(PropertyName = "icon")] public string Icon { get; init; }
    [JsonProperty(PropertyName = "enforcesSecureChat")] public bool SecureChat { get; init; }
    [JsonProperty(PropertyName = "previewsChat")] public bool PreviewsChat { get; init; }
    
    public class VersionPart
    {
        [JsonProperty(PropertyName = "name")] public string Name { get; init; }
        [JsonProperty(PropertyName = "protocol")] public int Protocol { get; init; }
    }

    public class PlayersPart
    {
        [JsonProperty(PropertyName = "max")] public int Max { get; init; }
        [JsonProperty(PropertyName = "online")] public int Online { get; init; }
        [JsonProperty(PropertyName = "sample")] public List<object> Sample = [];
    }
    
    public class DescriptionPart
    {
        [JsonProperty(PropertyName = "text")] public string Text { get; init; }
    }
}

