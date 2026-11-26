using AzdoQualityMetrics.Models;
namespace AzdoQualityMetrics.Services;
public sealed class KpiCalculationService
{
 public KpiResult Calculate(IEnumerable<WorkItemRecord> bugs, IEnumerable<WorkItemRecord> delivered) { var b=bugs.Count(); var items=delivered.ToList(); var effort=items.Sum(x=>x.Effort ?? 0); return new KpiResult(b,effort,items.Count,effort <= 0 ? 0 : b / effort * 100); }
}
