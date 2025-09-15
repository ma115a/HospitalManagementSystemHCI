using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Doctor.Views;

public partial class LaboratoryRequestsView : UserControl
{
    public LaboratoryRequestsView()
    {
        InitializeComponent();
    }


    private void GoToDashboard(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as DoctorWindow;
        parentWindow?.GoToDashboard();
    }
}