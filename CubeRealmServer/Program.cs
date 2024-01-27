using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace CubeRealmServer;

class Program
{
    public static void Main(string[] args)
    {
        ServiceProvider serviceProvider = new ServiceCollection()
            .AddLogging(builder => builder.AddSimpleConsole(options =>
            {
                options.ColorBehavior = LoggerColorBehavior.Enabled;
                options.IncludeScopes = true;
                options.TimestampFormat = "HH:mm:ss.fff ";
            }).SetMinimumLevel(LogLevel.Trace))
            .BuildServiceProvider();
    }
}