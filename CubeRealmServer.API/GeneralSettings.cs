namespace CubeRealmServer.API;

public class GeneralSettings
{
    public required int VersionProtocol { get; set; }
    public required int MaxPlayers { get; set; }
    public bool HideOnlinePlayers { get; set; }
    public string Motd { get; set; }
    public bool Whitelist { get; set; }
}