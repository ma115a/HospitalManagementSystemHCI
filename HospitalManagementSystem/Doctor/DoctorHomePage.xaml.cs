using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;

namespace HospitalManagementSystem;

public partial class DoctorHomePage : UserControl
{
    public DoctorHomePage()
    {
        InitializeComponent();
    }

    private void MedicalRecord_Click(Object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as DoctorWindow;
        parentWindow?.GoToMedicalRecords();
    }


    private void Prescription_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as DoctorWindow;
        parentWindow?.GoToPrescriptions();
    }
    
    private void LaboratoryResults_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as DoctorWindow;
        parentWindow?.GoToLaboratoryResults();
    }
    
    private void LaboratoryRequests_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as DoctorWindow;
        parentWindow?.GoToLaboratoryRequests();
    }

    private void Admissions_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as DoctorWindow;
        parentWindow?.GoToAdmissions();
    }

    private void Surgeries_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as DoctorWindow;
        parentWindow?.GoToSurgeries();
    }
}