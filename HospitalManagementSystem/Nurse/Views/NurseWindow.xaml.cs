using System.Windows;
using HospitalManagementSystem.Nurse.ViewModels;

namespace HospitalManagementSystem.Nurse.Views;

public partial class NurseWindow : Window
{
    public NurseWindow(NurseWindowViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    public void GoToDashboard()
    {
        NurseSwitcher.SelectedIndex = 0;
    }

    public void GoToAppointments()
    {
        NurseSwitcher.SelectedIndex = 1;
    }

    public void GoToScheduleAppointment()
    {
        NurseSwitcher.SelectedIndex = 2;
    }

    public void GoToAdmissionsAndBeds()
    {
        NurseSwitcher.SelectedIndex = 3;
    }

    public void GoToLab()
    {
        NurseSwitcher.SelectedIndex = 4;
    }

    public void GoToSurgeries()
    {
        NurseSwitcher.SelectedIndex = 5;
    }

    public void GoToPatients()
    {
        NurseSwitcher.SelectedIndex = 6;
    }
}