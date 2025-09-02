using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf.Transitions;

namespace HospitalManagementSystem;

public partial class MedicalRecordView : UserControl
{
    public MedicalRecordView()
    {
        InitializeComponent();
    }


    private void GoToDashboard_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as DoctorWindow;
        parentWindow?.GoToDashboard();
    }
}