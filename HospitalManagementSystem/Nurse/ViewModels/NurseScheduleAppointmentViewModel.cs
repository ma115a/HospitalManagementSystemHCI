
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Nurse.Services;

namespace HospitalManagementSystem.Nurse.ViewModels;


public partial class NurseScheduleAppointmentViewModel : ObservableObject, IActivable
{
    
    
    private readonly AppointmentService _appointmentService;
    private readonly PatientService _patientService;
    private readonly DoctorService _doctorService;


    [ObservableProperty] private ObservableCollection<patient> _patients = new();
    [ObservableProperty] private ObservableCollection<doctor> _doctors = new();
    
    
    [ObservableProperty] private patient? _selectedPatient;
    [ObservableProperty] private doctor? _selectedDoctor;

    [ObservableProperty] private DateTime? _newAppointmentDate;
    [ObservableProperty] private string? _newAppointmentTime;
    [ObservableProperty] private string? _newAppointmentNotes;
    
    [ObservableProperty] private appointment  _selectedAppointment;
   
   public bool IsLoaded { get; private set; }

    public NurseScheduleAppointmentViewModel(AppointmentService appointmentService, PatientService patientService, DoctorService doctorService)
    {
        _appointmentService = appointmentService;
        _patientService = patientService;
        _doctorService = doctorService;
    }



    private async Task LoadData()
    {
        Patients.Clear();
        var patients = await _patientService.GetAllPatients();
        foreach (var patient in patients)
        {
            Patients.Add(patient);
        }

        Doctors.Clear();
        var doctors = await _doctorService.GetAllDoctors();
        foreach (var doctor in doctors)
        {
            Doctors.Add(doctor);
        }

    }
    public async Task ActivateAsync()
    {
        await LoadData();
    }


    [RelayCommand]
    private async Task SaveAppointment()
    {
        if (SelectedPatient == null || SelectedDoctor == null) return;

        if (NewAppointmentDate is null) return;
        

        var time = TimeSpan.Zero;
        if (!string.IsNullOrWhiteSpace(NewAppointmentTime))
        {
            if (TimeSpan.TryParse(NewAppointmentTime, out var parsed))
            {
                time = parsed;
            }
        }

        var dateTime = NewAppointmentDate.Value.Date + time;

        SelectedAppointment = new appointment
        {

            date = dateTime,
            notes = NewAppointmentNotes
        };

        await _appointmentService.SaveAppointment(SelectedAppointment, SelectedDoctor, SelectedPatient);

    }
    
    
    
}