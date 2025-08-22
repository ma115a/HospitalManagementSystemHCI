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

namespace HospitalManagementSystem
{
    /// <summary>
    /// Interaction logic for DoctorHomePage.xaml
    /// </summary>
    public partial class DoctorHomePage : Window
    {
        public DoctorHomePage()
        {
            InitializeComponent();
        }

        private void GoHome_Click(object sender, RoutedEventArgs e) => ContentSwitcher.SelectedIndex = 0;

        private void OpenMedicalRecords_Click(object sender, RoutedEventArgs e) => ContentSwitcher.SelectedIndex = 1;
        private void OpenPrescription_Click(object sender, RoutedEventArgs e) => ContentSwitcher.SelectedIndex = 2;
        private void OpenLabRequest_Click(object sender, RoutedEventArgs e) => ContentSwitcher.SelectedIndex = 3;
        private void OpenLabResults_Click(object sender, RoutedEventArgs e) => ContentSwitcher.SelectedIndex = 4;
        private void OpenSurgeries_Click(object sender, RoutedEventArgs e) => ContentSwitcher.SelectedIndex = 5;
        private void OpenAdmission_Click(object sender, RoutedEventArgs e) => ContentSwitcher.SelectedIndex = 6;
        private void OpenRooms_Click(object sender, RoutedEventArgs e) => ContentSwitcher.SelectedIndex = 7;
    }
}
