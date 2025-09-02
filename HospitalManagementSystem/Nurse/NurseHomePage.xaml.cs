using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem;

public partial class NurseHomePage : UserControl
{
    public NurseHomePage()
    {
        InitializeComponent();
    }

    private void Appointments_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as NurseWindow;
        parentWindow?.GoToAppointments();
    }

    private void ScheduleAppointments_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as NurseWindow;
        parentWindow?.GoToScheduleAppointment();
    }

    private void Admissions_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as NurseWindow;
        parentWindow?.GoToAdmissionsAndBeds();
    }

    private void Lab_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as NurseWindow;
        parentWindow?.GoToLab();
    }

    private void Surgeries_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as NurseWindow;
        parentWindow?.GoToSurgeries();
    }

    private void Patients_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as NurseWindow;
        parentWindow?.GoToPatients();
    }
}