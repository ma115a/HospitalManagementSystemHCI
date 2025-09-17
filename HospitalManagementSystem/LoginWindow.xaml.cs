using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HospitalManagementSystem.Surgeon;
using HospitalManagementSystem.Admin;
using HospitalManagementSystem.Doctor.Views;
using HospitalManagementSystem.Nurse.Views;
using Microsoft.Extensions.DependencyInjection;


namespace HospitalManagementSystem
{
    public partial class LoginWindow : Window
    {


        private readonly IServiceProvider _sp;

        public LoginWindow(IServiceProvider sp, LoginWindowViewModel vm)
        {
            InitializeComponent();
            _sp = sp;
            DataContext = vm;
        }


        private void OnSignInClick(object sender, RoutedEventArgs e)
        {
            // var shell = _sp.GetRequiredService<SurgeonWindow>();  // window + VM from DI
            // var shell = _sp.GetRequiredService<DoctorWindow>();  // window + VM from DI
            var shell = _sp.GetRequiredService<NurseWindow>();
            // var shell = new AdminWindow();
            shell.Show();
            Close();
        }

        private void UsernameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
