using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem;

public partial class LabWorkerHomePage : UserControl
{
    public LabWorkerHomePage()
    {
        InitializeComponent();
    }

    private void Requests_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as LabWindow;
        parentWindow?.GoToRequests();
    }

    private void Results_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as LabWindow;
        parentWindow?.GoToResults();
    }

    private void History_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as LabWindow;
        parentWindow?.GoToHistory();
    }
    
    
}