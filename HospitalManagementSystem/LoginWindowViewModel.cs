




using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem;
using HospitalManagementSystem.Admin;
using HospitalManagementSystem.Admin.Services;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

public partial class LoginWindowViewModel : ObservableObject
{
    
    private LoggedInUser _loggedInUser;
    private readonly UserService  _userService;
    private readonly IServiceProvider _sp;
    
    public ISnackbarMessageQueue MessageQueue { get; set; }


    [ObservableProperty] private string? _username;
    [ObservableProperty]
    private string? _password;

    [ObservableProperty] private bool _isButtonEnabled;


    public LoginWindowViewModel(UserService userService, LoggedInUser loggedInUser, IServiceProvider sp)
    {
        _userService = userService;
        _loggedInUser = loggedInUser;
        _sp = sp;
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
        _sp.GetRequiredService<AdminWindow>().Show();
        return;
        try
        {
            var employee = await _userService.GetUser(Username, Password);
            if (employee is not null)
            {
                _loggedInUser.SetLoggedInEmployee(employee);
                if (_loggedInUser.LoggedInEmployee.nurse is not null)
                {
                    Console.WriteLine("nurse");
                }

                if (_loggedInUser.LoggedInEmployee.doctor is not null)
                {
                    Console.WriteLine("doctor");
                }

                if (_loggedInUser.LoggedInEmployee.laboratory_tehnician is not null)
                {
                    Console.WriteLine("lab");
                }

                if (_loggedInUser.LoggedInEmployee.surgeon is not null)
                {
                    Console.WriteLine("surgeon");
                }

                if (_loggedInUser.LoggedInEmployee.administrator is not null)
                {
                    Console.WriteLine("admin");
                }
                Console.WriteLine("uspjesno");
            }


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

    }

    
    






}