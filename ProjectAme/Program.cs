using Avalonia;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ProjectAme;

public static class Program {
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) {
        ServiceCollection sc = new();

        sc.AddLogging(builder => builder.AddDebug().SetMinimumLevel(LogLevel.Information));
        var sp = sc.BuildServiceProvider();

        Ioc.Default.ConfigureServices(sp);

        try {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        } catch (Exception e) {
            sp.GetRequiredService<ILoggerFactory>().CreateLogger("ProjectAme").LogError(e, "An error occured.");
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    private static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
