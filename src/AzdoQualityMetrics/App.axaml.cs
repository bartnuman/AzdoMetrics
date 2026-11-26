using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AzdoQualityMetrics.ViewModels;
using AzdoQualityMetrics.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace AzdoQualityMetrics;

public partial class App : Application
{
    private IHost? _host;
    public override void Initialize() => AvaloniaXamlLoader.Load(this);
    public override void OnFrameworkInitializationCompleted()
    {
        var configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(c => { c.Sources.Clear(); c.AddJsonFile(configPath, optional: false, reloadOnChange: true); })
            .UseSerilog((ctx, services, log) => log.MinimumLevel.Information().Enrich.FromLogContext().WriteTo.File(Path.Combine(AppContext.BaseDirectory, "logs", "log-.log"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).WriteTo.Debug())
            .ConfigureServices((ctx, services) => Bootstrap.Configure(services, ctx.Configuration))
            .Build();
        _host.Start();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow { DataContext = _host.Services.GetRequiredService<MainWindowViewModel>() };
            desktop.Exit += (_, _) => { _host.StopAsync().GetAwaiter().GetResult(); _host.Dispose(); Log.CloseAndFlush(); };
        }
        base.OnFrameworkInitializationCompleted();
    }
}
