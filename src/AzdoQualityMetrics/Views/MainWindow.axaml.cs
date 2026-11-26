using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AzdoQualityMetrics.ViewModels;
namespace AzdoQualityMetrics.Views;
public partial class MainWindow : Window
{
 public MainWindow(){InitializeComponent();Opened+=(_,_)=>{if(DataContext is MainWindowViewModel vm)vm.Attach(this);};}
 private void InitializeComponent()=>AvaloniaXamlLoader.Load(this);
}
