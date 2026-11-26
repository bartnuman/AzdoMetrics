using AzdoQualityMetrics.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
namespace AzdoQualityMetrics.Config;
public sealed class ConfigurationService : IConfigurationService
{
    private readonly string _path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
    public AzureDevOpsSettings Settings { get; }
    public ConfigurationService(IConfiguration configuration) => Settings = configuration.GetSection("AzureDevOps").Get<AzureDevOpsSettings>() ?? new();
    public async Task SaveConnectionAsync(string organization, string project, CancellationToken cancellationToken = default)
    {
        Settings.Organization = organization.Trim(); Settings.Project = project.Trim();
        var json = await File.ReadAllTextAsync(_path, cancellationToken);
        using var doc = JsonDocument.Parse(json);
        var root = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json) ?? new();
        var azure = new Dictionary<string, string> { ["Organization"] = Settings.Organization, ["Project"] = Settings.Project, ["Pat"] = Settings.Pat };
        root["AzureDevOps"] = JsonSerializer.SerializeToElement(azure);
        var output = JsonSerializer.Serialize(root, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_path, output, cancellationToken);
    }
}
