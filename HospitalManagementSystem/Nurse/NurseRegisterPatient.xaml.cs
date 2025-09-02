using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem;

public partial class NurseRegisterPatient : UserControl
{
    public NurseRegisterPatient()
    {
        InitializeComponent();
    }
    
    private void GoToDashboard_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as NurseWindow;
        parentWindow?.GoToDashboard();
    }
}