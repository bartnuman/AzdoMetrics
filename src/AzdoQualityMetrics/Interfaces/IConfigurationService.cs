using AzdoQualityMetrics.Config;
namespace AzdoQualityMetrics.Interfaces;
public interface IConfigurationService { AzureDevOpsSettings Settings { get; } Task SaveConnectionAsync(string organization, string project, CancellationToken cancellationToken = default); }
