using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
namespace AzdoQualityMetrics.Models;
public partial class AreaNode : ObservableObject { public AreaNode? Parent { get; set; } public int Id { get; init; } public string Name { get; init; } = ""; public string Path { get; init; } = ""; public ObservableCollection<AreaNode> Children { get; } = new(); [ObservableProperty] private bool isSelected; partial void OnIsSelectedChanged(bool value) { foreach (var child in Children) child.IsSelected = value; if (Parent is not null) { if (!value) Parent.IsSelected = false; else if (Parent.Children.All(x => x.IsSelected)) Parent.IsSelected = true; } } }
