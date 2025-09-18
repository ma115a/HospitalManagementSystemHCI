




using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem;
using HospitalManagementSystem.Admin;
using HospitalManagementSystem.Admin.Services;
using HospitalManagementSystem.Doctor.Views;
using HospitalManagementSystem.Nurse.Views;
using HospitalManagementSystem.Surgeon;
using HospitalManagementSystem.Utils;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

public partial class LoginWindowViewModel : ObservableObject
{
    
    private LoggedInUser _loggedInUser;
    private readonly UserService  _userService;
    private readonly IServiceProvider _sp;
    public event Action? RequestClose;
    
    public ISnackbarMessageQueue MessageQueue { get; set; }
    private readonly LocalizationManager _localizationManager;


    [ObservableProperty] private string? _username;
    [ObservableProperty]
    private string? _password;

    [ObservableProperty] private bool _isButtonEnabled;


    public LoginWindowViewModel(UserService userService, LoggedInUser loggedInUser, IServiceProvider sp )
    {
        _userService = userService;
        _loggedInUser = loggedInUser;
        _sp = sp;
        _localizationManager = App.HostApp.Services.GetRequiredService<LocalizationManager>();
        MessageQueue = new SnackbarMessageQueue();
    }


    partial void OnUsernameChanged(string? value)
    {
        if (value != "") IsButtonEnabled = true;
    }
    
    partial void OnPasswordChanged(string? value)
    {
        if (value != "") IsButtonEnabled = true;
    }


    [RelayCommand]
    private async Task Login()
    {
        if (Username is null) return;
        if (Password is null) return;
        // _sp.GetRequiredService<AdminWindow>().Show();
        // RequestClose?.Invoke();
        // return;
        try
        {
            var employee = await _userService.GetUser(Username, Password);
            if (employee is not null)
            {
                _loggedInUser.SetLoggedInEmployee(employee);
                if (_loggedInUser.LoggedInEmployee.nurse is not null)
                {
                    Console.WriteLine("nurse");
                    
                    _sp.GetRequiredService<NurseWindow>().Show();
                }

                if (_loggedInUser.LoggedInEmployee.doctor is not null)
                {
                    Console.WriteLine("doctor");
                    _sp.GetRequiredService<DoctorWindow>().Show();
                }

                if (_loggedInUser.LoggedInEmployee.laboratory_tehnician is not null)
                {
                    Console.WriteLine("lab");
                    _sp.GetRequiredService<LabWindow>().Show();
                }

                if (_loggedInUser.LoggedInEmployee.surgeon is not null)
                {
                    Console.WriteLine("surgeon");
                    _sp.GetRequiredService<SurgeonWindow>().Show();
                }

                if (_loggedInUser.LoggedInEmployee.administrator is not null)
                {
                    Console.WriteLine("admin");
                    _sp.GetRequiredService<AdminWindow>().Show();
                    // return;
                }
                Console.WriteLine("uspjesno");
                RequestClose?.Invoke();
            }
            else
            {
                
                MessageQueue.Enqueue(_localizationManager.GetString("userDoesNotExist"));
            }


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            MessageQueue.Enqueue(_localizationManager.GetString("invalidPassword"));
        }

    }

    
    






}