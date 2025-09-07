using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Nurse.Views;

public partial class NurseLab : UserControl
{
    public NurseLab()
    {
        InitializeComponent();
    }
    
    private void GoToDashboard_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as NurseWindow;
        parentWindow?.GoToDashboard();
    }
}