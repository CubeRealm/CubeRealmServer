using PluginAPI;

namespace Plugin;

public class PluginNode(IPlugin plugin, List<PluginNode> next)
{
    public IPlugin Plugin { get; } = plugin;
    public List<PluginNode> Next { get; } = next;

}