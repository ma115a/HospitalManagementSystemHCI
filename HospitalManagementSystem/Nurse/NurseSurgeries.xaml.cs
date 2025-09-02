using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem;

public partial class NurseSurgeries : UserControl
{
    public NurseSurgeries()
    {
        InitializeComponent();
    }
    
    private void GoToDashboard_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as NurseWindow;
        parentWindow?.GoToDashboard();
    }
}