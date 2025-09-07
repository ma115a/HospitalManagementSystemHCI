using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HospitalManagementSystem.Surgeon;
using HospitalManagementSystem.Admin;
using HospitalManagementSystem.Nurse.Views;
using Microsoft.Extensions.DependencyInjection;


namespace HospitalManagementSystem
{
    public partial class LoginWindow : Window
    {


        private readonly IServiceProvider _sp;

        public LoginWindow(IServiceProvider sp)
        {
            InitializeComponent();
            _sp = sp;
        }


        private void OnSignInClick(object sender, RoutedEventArgs e)
        {
            var shell = _sp.GetRequiredService<NurseWindow>();  // window + VM from DI
            shell.Show();
            Close();
        }

        private void UsernameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
