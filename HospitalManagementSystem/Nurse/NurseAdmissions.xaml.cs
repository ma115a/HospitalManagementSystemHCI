using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem;

public partial class NurseAdmissions : UserControl
{
    public NurseAdmissions()
    {
        InitializeComponent();
    }
    private void GoToDashboard_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as NurseWindow;
        parentWindow?.GoToDashboard();
    }
}