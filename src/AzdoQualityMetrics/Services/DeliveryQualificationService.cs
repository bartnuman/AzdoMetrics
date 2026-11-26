using AzdoQualityMetrics.Models;
namespace AzdoQualityMetrics.Services;
public sealed class DeliveryQualificationService
{
 public bool Qualifies(IEnumerable<WorkItemUpdate> updates, DateTime start, DateTime end, IReadOnlyCollection<string> selectedStates, DeliveryQualificationMode mode, out DateTime? qualifyingDate)
 {
  var transitions=updates.Where(x=>x.NewState!=null && selectedStates.Contains(x.NewState,StringComparer.OrdinalIgnoreCase)).OrderBy(x=>x.ChangedDate).ToList();
  var last=transitions.LastOrDefault(); qualifyingDate=mode==DeliveryQualificationMode.AnyDoneTransitionInPeriod ? transitions.Where(x=>x.ChangedDate.Date>=start.Date && x.ChangedDate.Date<=end.Date).Select(x=>(DateTime?)x.ChangedDate).LastOrDefault() : last?.ChangedDate;
  if(mode==DeliveryQualificationMode.AnyDoneTransitionInPeriod) return transitions.Any(x=>x.ChangedDate.Date>=start.Date && x.ChangedDate.Date<=end.Date);
  return last!=null && last.ChangedDate.Date>=start.Date && last.ChangedDate.Date<=end.Date;
 }
}
