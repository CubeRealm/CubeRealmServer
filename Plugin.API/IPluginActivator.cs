namespace PluginAPI;

public interface IPluginActivator
{
    void Action(string actionTemplate, Action<IPlugin> action);
}