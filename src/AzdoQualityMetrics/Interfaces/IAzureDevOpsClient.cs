using AzdoQualityMetrics.Models;
namespace AzdoQualityMetrics.Interfaces;
public interface IAzureDevOpsClient
{
 Task<bool> ConnectAsync(CancellationToken ct); Task<IReadOnlyList<AreaNode>> GetAreasAsync(CancellationToken ct); Task<IReadOnlyList<WorkItemDefinition>> GetWorkItemTypesAsync(CancellationToken ct); Task<IReadOnlyList<StateDefinition>> GetStatesAsync(CancellationToken ct);
 Task<IReadOnlyList<int>> QueryWorkItemIdsAsync(CalculationRequest request, bool bugs, CancellationToken ct); Task<IReadOnlyList<WorkItemRecord>> GetWorkItemsAsync(IReadOnlyList<int> ids, CancellationToken ct); Task<IReadOnlyList<WorkItemUpdate>> GetUpdatesAsync(int id, CancellationToken ct);
}
