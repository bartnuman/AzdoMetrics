# AzdoQualityMetrics

A .NET 8 / Avalonia desktop dashboard for Azure DevOps quality metrics: **registered bugs per 100 delivered effort**.

## Architecture

- Avalonia 11 UI with CommunityToolkit.Mvvm commands and observable view models.
- `IAzureDevOpsClient` isolates Azure DevOps REST calls.
- Calculation services isolate KPI, date-range and delivery-transition rules.
- Serilog writes rolling daily files under `logs/`.
- CSV export supports bugs, delivered work and KPI summary.

## Configuration

Edit `src/AzdoQualityMetrics/appsettings.json` before first launch:

```json
{
  "AzureDevOps": { "Organization": "your-org", "Project": "your-project", "Pat": "YOUR_PAT" }
}
```

The PAT is not exposed in the UI. Organization and Project are saved to the copied `appsettings.json` when Connect is used. Protect this file and never commit a real PAT.

## Running locally

Install the .NET 8 SDK, then run:

```bash
dotnet restore AzdoQualityMetrics.sln
dotnet run --project src/AzdoQualityMetrics/AzdoQualityMetrics.csproj
```

## Publishing

```bash
dotnet publish src/AzdoQualityMetrics/AzdoQualityMetrics.csproj -c Release -r win-x64 --self-contained true
```

## GitHub Actions

`.github/workflows/build.yml` restores, builds, tests and publishes Windows-x64 binaries as a workflow artifact.

## Azure DevOps permissions required

The PAT needs Work Items read access (at minimum the `vso.work` scope) and access to the target project. Project visibility and area path permissions must permit reading the relevant work items and classification nodes.

## CSV exports

Use the export actions in the Results tab to save Registered Bugs, Delivered Work, or KPI Summary CSV files. Files include column headers and UTF-8 encoding.

## Troubleshooting

- **401/403:** check PAT validity, `vso.work` scope and project permissions.
- **429:** retry after the Azure DevOps rate limit window.
- **No results:** check organization/project spelling, area selections, dates and completed states.
- **Missing Effort:** the item is skipped and a warning is recorded.
- **Configuration changes:** ensure the output directory's `appsettings.json` is writable.

## Screenshots

Add application screenshots here after running the application, for example `docs/screenshots/dashboard.png`.
