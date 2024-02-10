using System.Reflection;

namespace CubeRealmServer.API;

public class ModulesLoader
{
    /// <summary>Loads all plugins from a DLL file.</summary>
    /// <param name="fileName">The filename of a DLL, e.g. "C:\Prog\MyApp\MyPlugIn.dll"</param>
    /// <returns>A list of plugin objects.</returns>
    /// <remarks>One DLL can contain several types which implement `T`.</remarks>
    public static List<Type> FromFile<T>(string fileName)
    {
        Assembly asm = Assembly.LoadFrom(fileName);
        List<Type> modules = new();
        
        foreach (Type unknownType in asm.GetExportedTypes())
        {
            if (unknownType.IsAssignableTo(typeof(T)))
            {
                modules.Add(unknownType);
            }
        }
        return modules;
    }
}