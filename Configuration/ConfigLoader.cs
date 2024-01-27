using System.Reflection;
using ConfigurationAPI;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Configuration;

public class ConfigLoader : IConfigLoader
{

    private ILogger<ConfigLoader> Logger { get; }

    private Dictionary<string, List<string>> Resources => new Dictionary<string, List<string>>()
    {
        { "settings", ["mobs_spawn.json", "server.json", "world.json"] }
    };

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

    public void Save(object obj, string path)
    {
        path = Path.Combine(Directory.GetCurrentDirectory(), path);

        string output = JsonConvert.SerializeObject(obj);
        if(File.Exists(path)) return;

        try
        {
            File.Create(path);
            File.WriteAllText(path, output);
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            throw;
        }
    }

    public void SaveDefaults()
    {
        const string allResources = "Configuration.Resources.";
        
        foreach (var items in Resources)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), items.Key);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            
            foreach (var fileName in items.Value)
                SaveResource($"{fileName}", Path.Combine(filePath, fileName));
        }
    }

    private void SaveResource(string assemblyFile, string file, bool overrideFile = false)
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