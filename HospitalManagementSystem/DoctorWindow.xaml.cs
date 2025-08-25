using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.Input;


namespace HospitalManagementSystem
{
    /// <summary>
    /// Interaction logic for DoctorHomePage.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        public DoctorWindow()
        {
            InitializeComponent();
            DataContext = this;
            CurrentSlideIndex = 0;
            ContentSwitcher.SelectedIndex = 0;
        }
        
        public int CurrentSlideIndex { get; set; }
       

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
