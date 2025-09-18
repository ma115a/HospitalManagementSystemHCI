using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Data.Models;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Admin.ViewModels;

public partial class AdminWindowViewModel : ObservableObject
{

    [ObservableProperty]
    private int _currentSlideIndex;

    private readonly LoggedInUser _user;
    [ObservableProperty] private employee _employee;


    public AdminWindowViewModel()
    {
        _user = App.HostApp.Services.GetRequiredService<LoggedInUser>();
        Employee = _user.LoggedInEmployee;
        _user.EmployeeChanged += OnUserChanged;
    }


    private void OnUserChanged(employee value)
    {
        Console.WriteLine("OnUserChanged");
        Employee = _user.LoggedInEmployee;
    }

    [RelayCommand]
    private void GoToDashboard()
    {
        CurrentSlideIndex = 0;
    }


    [RelayCommand]
    private void GoToManageUsers()
    {
        CurrentSlideIndex = 1;
    }
    [RelayCommand]
    private void GoToDepartmentsRooms()
    {
        CurrentSlideIndex = 2;
    }

    [RelayCommand]
    private void GoToVehicles()
    {
        CurrentSlideIndex = 3;
    }


    [RelayCommand]
    private void GoToMedicationInventory()
    {
        CurrentSlideIndex = 4;
    }




}