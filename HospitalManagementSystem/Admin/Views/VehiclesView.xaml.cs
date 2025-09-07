using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Admin.Views;

public partial class VehiclesView : UserControl
{
    public VehiclesView()
    {
        InitializeComponent();
        DataContext = App.HostApp.Services.GetRequiredService<VehiclesViewModel>();
    }
    
    private void GoToDashboard(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as AdminWindow;
        parentWindow?.GoToDashboard();
    }
}