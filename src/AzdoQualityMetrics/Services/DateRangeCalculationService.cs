using AzdoQualityMetrics.Models;
namespace AzdoQualityMetrics.Services;
public sealed class DateRangeCalculationService
{
 public (DateTime Start, DateTime End) FromPeriod(DateTime end, int value, PeriodUnit unit) { if(value < 1) throw new ArgumentOutOfRangeException(nameof(value)); var start = unit switch { PeriodUnit.Days => end.Date.AddDays(-value + 1), PeriodUnit.Weeks => end.Date.AddDays(-7 * value + 1), PeriodUnit.Months => end.Date.AddMonths(-value).AddDays(1), PeriodUnit.Years => end.Date.AddYears(-value).AddDays(1), _ => end.Date }; return (start, end.Date); }
 public int PeriodValue(DateTime start, DateTime end, PeriodUnit unit) { if(end.Date < start.Date) throw new ArgumentException("End date must not precede start date."); return unit switch { PeriodUnit.Days => (end.Date-start.Date).Days+1, PeriodUnit.Weeks => Math.Max(1,(int)Math.Ceiling(((end.Date-start.Date).Days+1)/7d)), PeriodUnit.Months => Math.Max(1,(end.Year-start.Year)*12+end.Month-start.Month+1), PeriodUnit.Years => Math.Max(1,end.Year-start.Year+1), _ => 1 }; }
}
