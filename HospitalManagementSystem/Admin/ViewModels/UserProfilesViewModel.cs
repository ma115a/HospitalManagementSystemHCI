
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Admin.Services;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Utils;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Admin.ViewModels;

public partial class UserProfilesViewModel : ObservableObject 
{


    private readonly UserService _userService;
    private bool isEditing = false;
    
    
    public ISnackbarMessageQueue MessageQueue { get; set; }
    private readonly LocalizationManager _localizationManager;
    [ObservableProperty]
    private employee? selectedEmployee;

    [ObservableProperty] private ObservableCollection<employee> employees = new();

    // [ObservableProperty] private string fullName;
    // [ObservableProperty] private string username;
    // [ObservableProperty] private string password;
    // [ObservableProperty] private string email;
    // [ObservableProperty] private string phone;
    [ObservableProperty] private string password;
    [ObservableProperty] private string selectedRole;
    [ObservableProperty] private string selectedDepartment;


    [ObservableProperty] private bool isControlsEnabled = false;

    [ObservableProperty] private bool _isControls2Enabled = false;

    private bool RequiresDepartment(string role)
    {
        return role switch
        {
            "Doctor" => true,
            "Administrator" => false,
            "Surgeon" => true,
            "Lab Technician" => false,
            "Nurse" => true,
            _ => false,
        };
    }

     public bool IsDepartmentEnabled => IsControlsEnabled && RequiresDepartment(SelectedRole);
    
    
    public UserProfilesViewModel(UserService userService)
    {
        _userService = userService;
        
        _localizationManager = App.HostApp.Services.GetRequiredService<LocalizationManager>();
        MessageQueue = new SnackbarMessageQueue();
        _ = LoadData();
    }

    partial void OnSelectedRoleChanged(string value)
    {
        OnPropertyChanged(nameof(IsDepartmentEnabled));
    }
    partial void OnIsControlsEnabledChanged(bool value)
    {
        OnPropertyChanged(nameof(IsDepartmentEnabled));
    }


    partial void OnSelectedEmployeeChanged(employee? value)
    {
        if (value is not null) IsControls2Enabled = true;
        else IsControls2Enabled = false;
        if (value is null)
        {
            SelectedRole = null;
            SelectedDepartment = null;
            return;
        }

        if (value.doctor != null)
        {
            SelectedRole = "Doctor";
            SelectedDepartment = string.IsNullOrWhiteSpace(value.doctor.specialty) ? null : value.doctor.specialty;
            return;
        }
        if (value.nurse != null)
        {
            SelectedRole = "Nurse";
            SelectedDepartment = string.IsNullOrWhiteSpace(value.nurse.specialty) ? null : value.nurse.specialty;
            return;
        }

        if (value.surgeon != null)
        {
            SelectedRole = "Surgeon";
            SelectedDepartment = null;
            return;
        }

        if (value.laboratory_tehnician != null)
        {
            SelectedRole = "Lab Technician";
            SelectedDepartment = null;
            return;
        }
        
        SelectedRole = null;
        SelectedDepartment = null;
    }
    

    private async Task LoadData()
    {
       await LoadEmployeesAsync(); 
    }
    
    
    

    private async Task LoadEmployeesAsync()
    {
        try
        {
            var employeList = await _userService.GetAllEmployees();
            Employees.Clear();
            foreach (var employee in employeList)
            {
                Employees.Add(employee);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        
    }


    [RelayCommand]
    private void CancelEmployee()
    {
        SelectedEmployee = null;
        IsControlsEnabled = false;
        Password = "";
    }


    [RelayCommand]
    private void EditUser()
    {
        isEditing = true;
        IsControlsEnabled = true;
    }


    [RelayCommand]
    private async void DeleteUser()
    {
        if (SelectedEmployee != null)
        {
            await _userService.DeleteUser(SelectedEmployee);
            await  LoadEmployeesAsync();
            IsControlsEnabled = false;
        }
    }

    [RelayCommand]
    private void NewUser()
    {
        IsControlsEnabled = true;
        isEditing = false;
        Password = "";
        SelectedEmployee = new employee
        {
            name = string.Empty,
            username = string.Empty,
            email = string.Empty,
            phone = string.Empty,
            date_employed = null,
            password = string.Empty,
            notes = string.Empty,
            active = true,
        };
        SelectedDepartment = null;
        SelectedRole = null;
    }


    [RelayCommand]
    private async Task SaveEmployee()
    {
        if (SelectedEmployee == null) return;

        if (isEditing)
        {
            Console.WriteLine("editing mode");
            await _userService.UpdateUser(SelectedEmployee, Password, SelectedDepartment);
            await LoadEmployeesAsync();
            IsControlsEnabled = false;
        }
        else
        {
            try
            {
                var e = await _userService.CreateUser(SelectedEmployee, Password, SelectedRole, SelectedDepartment);
                if (e is null)
                {
                    Console.WriteLine("korisnik postoji");
                   MessageQueue.Enqueue(_localizationManager.GetString("userExists"));
                   return;
                }
                IsControlsEnabled = false;
                await LoadEmployeesAsync();
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message); 
            }

        }
    }
}
    
