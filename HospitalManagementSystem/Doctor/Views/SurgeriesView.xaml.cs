using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Doctor.Views;

public partial class SurgeriesView : UserControl
{
    public SurgeriesView()
    {
        InitializeComponent();
    }

    private void GoToDashboard(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as DoctorWindow;
        parentWindow?.GoToDashboard();
    }
}