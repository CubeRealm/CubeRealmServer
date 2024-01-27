using System.Reflection;
using Configuration.Configurations;
using ConfigurationAPI;
using ConfigurationAPI.Configurations;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Configuration;

public class ConfigLoader : IConfigLoader
{
    
    private ILogger<ConfigLoader> Logger { get; }

    private ServerConfig _serverConfig;
    private WorldConfig _worldConfig;
    private MobsSpawnConfig _mobsSpawnConfig;
    
    public IServerConfig ServerConfig => _serverConfig;
    public IWorldConfig WorldConfig => _worldConfig;
    public IMobsSpawnConfig MobsSpawnConfig => _mobsSpawnConfig;

    private Dictionary<string, List<string>> Resources => new()
    {
        { "settings", ["mobs_spawn.json", "server.json", "world.json"] }
    };

    public ConfigLoader(ILogger<ConfigLoader> logger)
    {
        Logger = logger;
    }
    
    public void LoadConfigs(string path)
    {
        _serverConfig = Load<ServerConfig>(Path.Combine(path, "server.json"));
        _worldConfig = Load<WorldConfig>(Path.Combine(path, "world.json"));
        _mobsSpawnConfig = Load<MobsSpawnConfig>(Path.Combine(path, "mobs_spawn.json"));
    }

    public T? Load<T>(string path)
    {
        try
        {
            Logger.LogTrace($"{path} Loaded successful");
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            return default;
        }
    }

    public void SaveDefaults(string saveToDir)
    {
        const string allResources = "Configuration.Resources.";
        
        foreach (var items in Resources)
        {
            string filePath = Path.Combine(saveToDir, items.Key);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            
            foreach (var fileName in items.Value)
                SaveResource($"{fileName}", Path.Combine(filePath, fileName));
        }
    }

    private void SaveResource(string assemblyFile, string file)
    {
        if (File.Exists(file))
            return;
        Logger.LogTrace("assemblyFile {} file {}", assemblyFile, file);
        Assembly assembly = Assembly.GetExecutingAssembly();
        Stream? stream = assembly.GetManifestResourceStream(assemblyFile);
        if (stream == null)
            throw new NullReferenceException("Resource not found");
        using (var fileStream = new FileStream(file, FileMode.CreateNew))
            for (int i = 0; i < stream.Length; i++)
                fileStream.WriteByte((byte)stream.ReadByte());
    }
    
}