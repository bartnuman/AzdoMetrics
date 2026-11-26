using AzdoQualityMetrics.AzureDevOps;
using AzdoQualityMetrics.Config;
using AzdoQualityMetrics.Export;
using AzdoQualityMetrics.Interfaces;
using AzdoQualityMetrics.Services;
using AzdoQualityMetrics.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace AzdoQualityMetrics;

public static class Bootstrap
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConfigurationService, ConfigurationService>();
        services.AddSingleton<IAzureDevOpsClient, AzureDevOpsClient>();
        services.AddSingleton<KpiCalculationService>();
        services.AddSingleton<DateRangeCalculationService>();
        services.AddSingleton<DeliveryQualificationService>();
        services.AddSingleton<CsvExportService>();
        services.AddSingleton<MainWindowViewModel>();
    }
}
