namespace CubeRealmServer;

public static class Utils
{
    public static TR GetOriginalService<T, TR>(this IServiceProvider provider) where TR : T 
    {
        return (TR)provider.GetService(typeof(T))!;
    }
}