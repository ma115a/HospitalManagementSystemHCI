using System.Windows;
using HospitalManagementSystem.Doctor.ViewModels;

namespace HospitalManagementSystem.Doctor.Views
{
    /// <summary>
    /// Interaction logic for DoctorHomePage.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        public DoctorWindow(DoctorWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public void GoToDashboard()
        {
            ContentSwitcher.SelectedIndex = 0;
        }

        public void GoToMedicalRecords()
        {
            ContentSwitcher.SelectedIndex = 1;
        }

        public void GoToPrescriptions()
        {
            ContentSwitcher.SelectedIndex = 2;
        }

        public void GoToLaboratoryResults()
        {
            ContentSwitcher.SelectedIndex = 3;
        }

        public void GoToLaboratoryRequests()
        {
            ContentSwitcher.SelectedIndex = 4;
        }

        public void GoToAdmissions()
        {
            ContentSwitcher.SelectedIndex = 5;
        }

        public void GoToSurgeries()
        {
            ContentSwitcher.SelectedIndex = 6;
        }


       

       
    }
}
