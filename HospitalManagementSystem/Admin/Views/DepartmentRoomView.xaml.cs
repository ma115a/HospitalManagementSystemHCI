using System.Windows;
using System.Windows.Controls;
using HospitalManagementSystem.Admin;

namespace HospitalManagementSystem.Admin.Views;

public partial class DepartmentRoomView : UserControl
{
    public DepartmentRoomView()
    {
        InitializeComponent();
    }
    
    private void GoToDashboard(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as AdminWindow;
        parentWindow?.GoToDashboard();
    }
}