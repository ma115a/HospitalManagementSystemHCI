using System.Windows;

namespace HospitalManagementSystem.Admin;

public partial class AdminWindow : Window
{
    public AdminWindow()
    {
        InitializeComponent();
        DataContext = new ViewModels.AdminWindowViewModel();
    }

    public void GoToDashboard()
    {
        AdminSwitcher.SelectedIndex = 0;
    }

    public void GoToUserProfiles()
    {
        AdminSwitcher.SelectedIndex = 1;
    }

    public void GoToDepartmentsView()
    {
        AdminSwitcher.SelectedIndex = 2;
    }

    public void GoToVehiclesView()
    {
        AdminSwitcher.SelectedIndex = 3;
    }

    public void GoToMedicationView()
    {
        AdminSwitcher.SelectedIndex = 4;
    }


}
