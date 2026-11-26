using AzdoQualityMetrics.Models;
using AzdoQualityMetrics.Services;
using AzdoQualityMetrics.Export;
using Xunit;
namespace AzdoQualityMetrics.Tests;
public class CalculationTests
{
 [Fact] public void Kpi_IsBugsPerHundredEffort(){var s=new KpiCalculationService();var r=s.Calculate(Enumerable.Range(1,10).Select(i=>new WorkItemRecord{Id=i}),new[]{new WorkItemRecord{Effort=20},new WorkItemRecord{Effort=30}});Assert.Equal(10,r.Bugs);Assert.Equal(50,r.Effort);Assert.Equal(20,r.BugsPer100Effort);}
 [Fact] public void Kpi_ZeroEffortReturnsZero(){var r=new KpiCalculationService().Calculate(new[]{new WorkItemRecord()},Array.Empty<WorkItemRecord>());Assert.Equal(0,r.BugsPer100Effort);}
 [Theory][InlineData(PeriodUnit.Weeks,8,56)][InlineData(PeriodUnit.Days,8,8)][InlineData(PeriodUnit.Months,1,31)] public void DateRange_PeriodIsInclusive(PeriodUnit unit,int amount,int expectedDays){var range=new DateRangeCalculationService().FromPeriod(new DateTime(2026,10,31),amount,unit);Assert.Equal(expectedDays,(range.End-range.Start).Days+1);}
 [Fact] public void PeriodValue_RejectsInvalidRange(){Assert.Throws<ArgumentException>(()=>new DateRangeCalculationService().PeriodValue(DateTime.Today.AddDays(1),DateTime.Today,PeriodUnit.Days));}
 [Fact] public void AnyTransition_QualifiesIfAnySelectedStateTransitionInRange(){var u=new[]{new WorkItemUpdate(1,new DateTime(2026,1,2),"Active","Done"),new WorkItemUpdate(1,new DateTime(2026,3,2),"Done","Active")};var ok=new DeliveryQualificationService().Qualifies(u,new DateTime(2026,1,1),new DateTime(2026,1,31),new[]{"Done"},DeliveryQualificationMode.AnyDoneTransitionInPeriod,out _);Assert.True(ok);}
 [Fact] public void LastTransition_RequiresLatestInsideRange(){var u=new[]{new WorkItemUpdate(1,new DateTime(2026,1,2),"Active","Done"),new WorkItemUpdate(1,new DateTime(2026,3,2),"Active","Done")};var ok=new DeliveryQualificationService().Qualifies(u,new DateTime(2026,1,1),new DateTime(2026,1,31),new[]{"Done"},DeliveryQualificationMode.LastDoneTransitionInPeriod,out _);Assert.False(ok);}
 [Fact] public async Task Csv_WritesHeaderAndEscapesValues(){var p=Path.GetTempFileName();try{await new CsvExportService().ExportBugsAsync(p,new[]{new WorkItemRecord{Id=1,Title="a,b"}});var t=await File.ReadAllTextAsync(p);Assert.Contains("\"Id\"",t);Assert.Contains("\"a,b\"",t);}finally{File.Delete(p);}}
}
