



using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Nurse.Views;
using HospitalManagementSystem.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Nurse.ViewModels;

public partial class NurseWindowViewModel : ObservableObject
{
    
    private readonly IServiceProvider _sp;
    private readonly Dictionary<int, Lazy<IActivable>> _slides;
    
    private LoggedInUser _user;
    [ObservableProperty] private employee _employee;
    
    
    
    public NurseWindowViewModel(IServiceProvider sp)
    {
        _sp = sp;
        _user = App.HostApp.Services.GetRequiredService<LoggedInUser>();
        Employee = _user.LoggedInEmployee;
        _user.EmployeeChanged += OnUserChanged;

        _slides = new()
        {
            { 1, new Lazy<IActivable>(() => _sp.GetRequiredService<NurseAppointmentsViewModel>()) },
            { 2, new Lazy<IActivable>(() => _sp.GetRequiredService<NurseScheduleAppointmentViewModel>()) },
            { 3, new Lazy<IActivable>(() => _sp.GetRequiredService<NurseAdmissionsViewModel>()) },
            { 4, new Lazy<IActivable>(() => _sp.GetRequiredService<NurseLabViewModel>()) },
            { 5, new Lazy<IActivable>(() => _sp.GetRequiredService<NurseSurgeriesViewModel>()) },
            { 6, new Lazy<IActivable>(() => _sp.GetRequiredService<NurseRegisterPatientViewModel>()) },
        };
    }    
    private void OnUserChanged(employee value)
    {
        Console.WriteLine("OnUserChanged");
        Employee = _user.LoggedInEmployee;
    }


    
    
    public NurseRegisterPatientViewModel? PatientsVm
        => _slides.TryGetValue(6, out var l) ? (NurseRegisterPatientViewModel) l.Value : null;
    
    
    
    public NurseScheduleAppointmentViewModel? ScheduleVm
        => _slides.TryGetValue(2, out var l) ? (NurseScheduleAppointmentViewModel) l.Value : null;
    public NurseAppointmentsViewModel? AppointmentsVm
        => _slides.TryGetValue(1, out var l) ?  (NurseAppointmentsViewModel) l.Value : null;
    
    public NurseAdmissionsViewModel? AdmissionVm
        => _slides.TryGetValue(3, out var l) ? (NurseAdmissionsViewModel) l.Value : null;
    
    
    public NurseLabViewModel? LabVm
        => _slides.TryGetValue(4, out var l) ? (NurseLabViewModel) l.Value : null;
    
    [ObservableProperty]
    private int _currentSlideIndex;
    partial void OnCurrentSlideIndexChanged(int value)
    {
        _ = ActivateSelectedAsync(value);
    }

    private async Task ActivateSelectedAsync(int index)
    {
        if (_slides.TryGetValue(index, out var lazy))
        {
            var vm = lazy.Value;
            await vm.ActivateAsync();
        }
    }

    [RelayCommand]
    private void GoToDashboard()
    {
        CurrentSlideIndex = 0;
    }


    [RelayCommand]
    private void GoToAppointments()
    {
        CurrentSlideIndex = 1;
    }
    [RelayCommand]
    private void GoToScheduleAppointment()
    {
        CurrentSlideIndex = 2;
    }

    [RelayCommand]
    private void GoToAdmissions()
    {
        CurrentSlideIndex = 3;
    }


    [RelayCommand]
    private void GoToLab()
    {
        CurrentSlideIndex = 4;
    }
    
    
    [RelayCommand]
    private void GoToSurgeries()
    {
        CurrentSlideIndex = 5;
    }
    
    [RelayCommand]
    private void GoToPatients()
    {
        CurrentSlideIndex = 6;
    }
    
    
}