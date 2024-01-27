using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PluginAPI;

public static class PluginLoader
{
    public static IServiceCollection AddFromDirectory(this IServiceCollection serviceCollection, string directory, Action<IServiceCollection, Type> forAdd)
    {
        string[] files = Directory.GetFiles(directory);
        
        foreach (string file in files)
        {
            if (!file.EndsWith(".dll"))
                continue;
            AddFromFile(serviceCollection, file, forAdd);
        }
        return serviceCollection;
    }
    
    public static IServiceCollection AddFromFile(this IServiceCollection serviceCollection, string fileName, Action<IServiceCollection, Type> forAdd)
    {
        Assembly asm = Assembly.LoadFrom(fileName);
        
        foreach (Type unknownType in asm.GetExportedTypes())
        {
            if (unknownType.IsAssignableTo(typeof(IPlugin)))
            {
                forAdd(serviceCollection, unknownType);
            }
        }

        return serviceCollection;
    }
}