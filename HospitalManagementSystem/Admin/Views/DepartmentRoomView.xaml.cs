using System.Windows;
using System.Windows.Controls;
using HospitalManagementSystem.Admin;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Admin.Views;

public partial class DepartmentRoomView : UserControl
{
    public DepartmentRoomView()
    {
        InitializeComponent();
        DataContext = App.HostApp.Services.GetRequiredService<DepartmentRoomViewModel>();
    }
    
    private void GoToDashboard(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as AdminWindow;
        parentWindow?.GoToDashboard();
    }
}