using ConfigurationAPI;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Configuration;

public class ConfigLoader : IConfigLoader
{

    private ILogger<ConfigLoader> Logger { get; }

    public ConfigLoader(ILogger<ConfigLoader> logger)
    {
        Logger = logger;
    }

    public T? Load<T>(string path)
    {
        path = Path.Combine(Directory.GetCurrentDirectory(), path);

        try
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            return default;
        }
    }
    
    public void Save(object obj)
    
}