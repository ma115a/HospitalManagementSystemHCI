using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Admin.Views;

public partial class VehiclesView : UserControl
{
    public VehiclesView()
    {
        InitializeComponent();
    }
    
    private void GoToDashboard(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as AdminWindow;
        parentWindow?.GoToDashboard();
    }
}