using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem;

public partial class LabWorkerRequests : UserControl
{
    public LabWorkerRequests()
    {
        InitializeComponent();
    }

    private void GoToDashboard_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as LabWindow;
        parentWindow?.GoToDashboard();
    }
}