using System.Windows;
using Serilog;

namespace Aurelia.VenueStudio;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        Log.Logger = new LoggerConfiguration().MinimumLevel.Information().CreateLogger();
        Log.Information("Venue Studio {Version} starting", typeof(App).Assembly.GetName().Version);
        base.OnStartup(e);
    }
}
