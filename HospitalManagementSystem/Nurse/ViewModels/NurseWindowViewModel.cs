



using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Nurse.ViewModels;

public partial class NurseWindowViewModel : ObservableObject
{
    
    private readonly IServiceProvider _sp;
    private readonly Dictionary<int, Lazy<IActivable>> _slides;
    
    
    
    public NurseWindowViewModel(IServiceProvider sp)
    {
        _sp = sp;
        

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
    
    
    public NurseRegisterPatientViewModel? PatientsVm
        => _slides.TryGetValue(6, out var l) ? (NurseRegisterPatientViewModel) l.Value : null;
    
    
    
    public NurseScheduleAppointmentViewModel? ScheduleVm
        => _slides.TryGetValue(2, out var l) ? (NurseScheduleAppointmentViewModel) l.Value : null;
    public NurseAppointmentsViewModel? AppointmentsVm
        => _slides.TryGetValue(1, out var l) ?  (NurseAppointmentsViewModel) l.Value : null;
    
    public NurseAdmissionsViewModel? AdmissionVm
        => _slides.TryGetValue(3, out var l) ? (NurseAdmissionsViewModel) l.Value : null;
    
    [ObservableProperty]
    private int _currentSlideIndex;
    partial void OnCurrentSlideIndexChanged(int value)
    {
        Console.WriteLine("index changed");
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
        Console.WriteLine("Schedule Appointment");
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