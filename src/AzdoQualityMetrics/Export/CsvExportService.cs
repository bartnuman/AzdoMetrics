using AzdoQualityMetrics.Models;
using System.Globalization;
using System.Text;
namespace AzdoQualityMetrics.Export;
public sealed class CsvExportService
{
 public async Task ExportBugsAsync(string path,IEnumerable<WorkItemRecord> rows,CancellationToken ct=default) => await WriteAsync(path,new[]{"Id","Title","CreatedDate","AreaPath"},rows.Select(x=>new[]{x.Id.ToString(),x.Title,x.CreatedDate?.ToString("O"),x.AreaPath}),ct);
 public async Task ExportDeliveredAsync(string path,IEnumerable<WorkItemRecord> rows,CancellationToken ct=default) => await WriteAsync(path,new[]{"Id","WorkItemType","Title","Effort","DoneDate","QualificationMode"},rows.Select(x=>new[]{x.Id.ToString(),x.WorkItemType,x.Title,x.Effort?.ToString(CultureInfo.InvariantCulture),x.DoneDate?.ToString("O"),x.QualificationMode}),ct);
 public async Task ExportKpiAsync(string path,KpiResult k,CancellationToken ct=default) => await WriteAsync(path,new[]{"RegisteredBugs","DeliveredEffort","DeliveredWorkItems","BugsPer100Effort"},new[]{new[]{k.Bugs.ToString(),k.Effort.ToString(CultureInfo.InvariantCulture),k.DeliveredWorkItems.ToString(),k.BugsPer100Effort.ToString("0.00",CultureInfo.InvariantCulture)}},ct);
 private static async Task WriteAsync(string path,IEnumerable<string> headers,IEnumerable<string?[]> rows,CancellationToken ct) { var sb=new StringBuilder(); sb.AppendLine(string.Join(",",headers.Select(Escape))); foreach(var row in rows) sb.AppendLine(string.Join(",",row.Select(Escape))); await File.WriteAllTextAsync(path,sb.ToString(),new UTF8Encoding(true),ct); }
 private static string Escape(string? value)
    {
        value ??= string.Empty;
        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }
}
