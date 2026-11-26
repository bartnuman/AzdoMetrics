namespace AzdoQualityMetrics.Models;
public enum DeliveryQualificationMode { AnyDoneTransitionInPeriod, LastDoneTransitionInPeriod }
public enum PeriodUnit { Days, Weeks, Months, Years }
public sealed record CalculationRequest(DateTime StartDate, DateTime EndDate, IReadOnlyList<string> AreaPaths, IReadOnlyList<string> WorkItemTypes, IReadOnlyList<string> CompletedStates, DeliveryQualificationMode QualificationMode);
public sealed record KpiResult(int Bugs, double Effort, int DeliveredWorkItems, double BugsPer100Effort);
public sealed record CalculationResult(KpiResult Kpi, IReadOnlyList<WorkItemRecord> Bugs, IReadOnlyList<WorkItemRecord> Delivered, TimeSpan Duration, int RetrievedCount);
public sealed record WorkItemUpdate(int Id, DateTime ChangedDate, string? OldState, string? NewState);
