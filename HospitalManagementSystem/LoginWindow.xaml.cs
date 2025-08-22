using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


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
            var shell = new DoctorHomePage();
            shell.Show();
            Close();
        }

        private void UsernameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}