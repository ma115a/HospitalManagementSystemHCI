using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HospitalManagementSystem.Surgeon;


namespace HospitalManagementSystem
{
    public partial class LoginWindow : Window
    {
        


        public LoginWindow()
        {
            InitializeComponent();
        }


        private async void OnSignInClick(object sender, RoutedEventArgs e)
        {
            // var shell = new DoctorWindow();
            // shell.Show();
            // Close();
            // var shell = new AdminWindow();
            //shell.Show();
            //Close();
            // var shell = new NurseWindow();
            // shell.Show();
            // Close();
            // var shell = new LabWindow();
            // shell.Show();
            // Close();
            var shell = new SurgeonWindow();
            shell.Show();
            Close();
        }

        private void UsernameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}