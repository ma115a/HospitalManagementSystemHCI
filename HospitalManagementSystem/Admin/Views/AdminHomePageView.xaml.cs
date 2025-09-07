using System.Windows;
using System.Windows.Controls;
using HospitalManagementSystem.Admin;

namespace HospitalManagementSystem.Admin.Views;

public partial class AdminHomePageView : UserControl
{
    public AdminHomePageView()
    {
        InitializeComponent();
    }

    private void ManageUserProfiles_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as AdminWindow;
        parentWindow?.GoToUserProfiles();
    }

    private void DepartmentRooms_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as AdminWindow;
        parentWindow?.GoToDepartmentsView();
    }

    private void Vehicles_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as AdminWindow;
        parentWindow?.GoToVehiclesView();
    }

    private void MedicationInventory_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as AdminWindow;
        parentWindow?.GoToMedicationView();
    }
}
