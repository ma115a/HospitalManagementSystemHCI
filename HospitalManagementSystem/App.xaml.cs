using System.Configuration;
using System.Data;
using System.Net.Security;
using System.Windows;
using System.Windows.Media;
using HospitalManagementSystem.Admin;
using HospitalManagementSystem.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Admin.Services;
using HospitalManagementSystem.Admin.ViewModels;
using HospitalManagementSystem.Doctor.ViewModels;
using HospitalManagementSystem.Doctor.Views;
using HospitalManagementSystem.LabWorker.ViewModels;
using HospitalManagementSystem.Nurse.Services;
using HospitalManagementSystem.Nurse.ViewModels;
using HospitalManagementSystem.Nurse.Views;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Surgeon;
using HospitalManagementSystem.Surgeon.ViewModels;
using HospitalManagementSystem.Utils;
using MaterialDesignThemes.Wpf;
using SurgeriesViewViewModel = HospitalManagementSystem.Surgeon.ViewModels.SurgeriesViewViewModel;
using HospitalManagementSystem.Properties;

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


                services.AddSingleton<ThemeService>();
                
                
                //admin
                services.AddSingleton<UserService>();
                services.AddSingleton<VehiclesService>();
                services.AddSingleton<DepartmentService>();
                services.AddTransient<AdminWindow>();
                services.AddTransient<AdminWindowViewModel>();
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
                
                
                
                //doctor

                services.AddTransient<DoctorWindow>();
                services.AddSingleton<SharedDataService>();


                // services.AddTransient<DoctorTopPanelViewModel>();
                // services.AddTransient<DoctorTopPanel>();
                services.AddTransient<DoctorWindowViewModel>();
                services.AddTransient<DoctorHomePageViewModel>();
                services.AddTransient<LaboratoryRequestsViewViewModel>();
                services.AddTransient<LaboratoryResultsViewViewModel>();
                services.AddTransient<MakeAdmissionViewViewModel>();
                services.AddTransient<MedicalRecordViewViewModel>();
                services.AddTransient<PrescriptionViewViewModel>();
                services.AddTransient<DoctorSurgeriesViewViewModel>();
                
                
                
                
                //surgeon
                services.AddSingleton<SurgeryService>();
                services.AddTransient<SurgeonWindow>();
                services.AddTransient<SurgeonWindowViewModel>();
                services.AddTransient<ScheduleSurgeryViewModel>() ;
                services.AddTransient<SurgeriesHistoryViewViewModel>();
                services.AddTransient<SurgeriesViewViewModel>();
                
                
                //lab tehnician
                services.AddTransient<LabWindow>();
                
                
                services.AddTransient<LabWindowViewModel>();
                services.AddTransient<LabWorkerRequestsViewModel>();
                services.AddTransient<LabWorkerResultsViewModel>();
                services.AddTransient<LabWorkerHistoryViewModel>();
                
                services.AddTransient<LoginWindow>();
                services.AddSingleton<LoggedInUser>();
                services.AddTransient<LoginWindowViewModel>();
                
                
                
                services.AddSingleton<LocalizationManager>();
            }).Build();


    protected override async void OnStartup(StartupEventArgs e)
    {
        await HostApp.StartAsync();
        base.OnStartup(e);

        
        var themeService = HostApp.Services.GetRequiredService<ThemeService>();
        
        

        
        
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