using CommunityToolkit.Mvvm.ComponentModel;
namespace AzdoQualityMetrics.Models;
public partial class SelectableOption : ObservableObject
{
    public string Name { get; }
    [ObservableProperty] private bool isSelected;
    public SelectableOption(string name, bool selected = false) { Name = name; isSelected = selected; }
}
