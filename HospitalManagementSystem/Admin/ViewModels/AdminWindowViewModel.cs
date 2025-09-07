using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HospitalManagementSystem.Admin.ViewModels;

public partial class AdminWindowViewModel : ObservableObject
{

    [ObservableProperty]
    private int _currentSlideIndex;

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