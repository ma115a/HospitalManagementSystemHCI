using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem;

public partial class MakeAdmissionView : UserControl
{
    public MakeAdmissionView()
    {
        InitializeComponent();
    }
    
    private void GoToDashboard_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as DoctorWindow;
        parentWindow?.GoToDashboard();
    }
}