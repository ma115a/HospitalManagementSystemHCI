using System.Configuration;
using System.Data;
using System.Windows;
using HospitalManagementSystem.Admin;
using HospitalManagementSystem.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Admin.Services;
using HospitalManagementSystem.Admin.ViewModels;
using HospitalManagementSystem.Nurse.Services;
using HospitalManagementSystem.Nurse.ViewModels;
using HospitalManagementSystem.Nurse.Views;
using HospitalManagementSystem.Services;

namespace HospitalManagementSystem;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IHost HostApp { get; } = 
        Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(cfg => 
                cfg.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true))
            .ConfigureServices((ctx, services) =>
            {
                var cs = ctx.Configuration.GetConnectionString("Hms");
                if (string.IsNullOrWhiteSpace(cs))
                    throw new InvalidOperationException("Connection string 'Hms' is missing or empty.");

                var serverVersion = new MySqlServerVersion(new Version(8, 0, 36)); // adjust to your MySQL/MariaDB
                services.AddDbContextFactory<HmsDbContext>(opt =>
                    opt.UseMySql(
                        cs + "SslMode=None;AllowPublicKeyRetrieval=True;", // helpful for MySQL 8 dev defaults
                        serverVersion,
                        o => o.EnableRetryOnFailure()));
                //admin
                services.AddSingleton<UserService>();
                services.AddSingleton<VehiclesService>();
                services.AddSingleton<DepartmentService>();
                services.AddTransient<AdminWindow>();
                services.AddTransient<UserProfilesViewModel>();
                services.AddTransient<VehiclesViewModel>();
                services.AddTransient<DepartmentRoomViewModel>();
                
                
                //nurse
                services.AddSingleton<PatientService>();
                services.AddSingleton<AppointmentService>();
                services.AddSingleton<DoctorService>();
                services.AddSingleton<AdmissionService>();
                services.AddSingleton<LaboratoryTestService>();

                services.AddTransient<NurseWindow>();
                services.AddTransient<NurseWindowViewModel>();
                services.AddTransient<NurseRegisterPatientViewModel>();
                services.AddSingleton<NurseScheduleAppointmentViewModel>();
                services.AddSingleton<NurseAppointmentsViewModel>();
                services.AddSingleton<NurseAdmissionsViewModel>();
                services.AddSingleton<NurseLabViewModel>();
                services.AddSingleton<NurseSurgeriesViewModel>();
                
                
                
                
                
                services.AddTransient<LoginWindow>();
            }).Build();


    protected override async void OnStartup(StartupEventArgs e)
    {
        Console.WriteLine("alo e");
        await HostApp.StartAsync();
        base.OnStartup(e);
        
        var login = HostApp.Services.GetRequiredService<LoginWindow>();
        login.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await HostApp.StopAsync();
        HostApp.Dispose();
        base.OnExit(e);
    }
    
}