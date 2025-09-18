


using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Nurse.Services;
using HospitalManagementSystem.Utils;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Nurse.ViewModels;

public partial class NurseAppointmentsViewModel : ObservableObject,  IActivable
{
    private readonly AppointmentService _appointmentService;
    private readonly DoctorService _doctorService;
    private readonly PatientService _patientService;
    public ISnackbarMessageQueue MessageQueue { get; set; }
    private readonly LocalizationManager _localizationManager;
    
    public bool IsLoaded { get; private set; }
    [ObservableProperty] private bool _isControlsEnabled = false;
    
    
    
    
    
    [ObservableProperty]
    private ObservableCollection<appointment>  _appointments = new ();
    [ObservableProperty]
    private appointment? _selectedAppointment;
    
    [ObservableProperty]
    private ObservableCollection<doctor> _doctors = new ();
    
    [ObservableProperty]
    private ObservableCollection<patient>  _patients = new ();
    [ObservableProperty] private DateTime? _newAppointmentDate;
    [ObservableProperty] private string?   _newAppointmentTime;   
    
    [ObservableProperty]
    private patient? _selectedPatient;
    [ObservableProperty]
    private doctor? _selectedDoctor;
    
    [ObservableProperty]
    private ICollectionView _appointmentsView;

    [ObservableProperty] private doctor? _selectedDoctorFilter;
    [ObservableProperty] private DateTime? _filterDate;
    [ObservableProperty] private string? _filterStatus;


    [ObservableProperty] private bool _isControls2Enabled = false;

    public NurseAppointmentsViewModel(AppointmentService appointmentService, DoctorService doctorService, PatientService patientService)
    {
       _appointmentService = appointmentService; 
       _doctorService = doctorService;
       _patientService = patientService;
       
       _localizationManager = App.HostApp.Services.GetRequiredService<LocalizationManager>();
       MessageQueue = new SnackbarMessageQueue();
       AppointmentsView = CollectionViewSource.GetDefaultView(Appointments);
       AppointmentsView.Filter = AppointmentsFilter;
       AppointmentsView.SortDescriptions.Add(
           new SortDescription(nameof(appointment.date), ListSortDirection.Ascending));
    }

    private bool AppointmentsFilter(object? obj)
    {
        if (obj is not appointment a) return false;
        if (SelectedDoctorFilter is not null &&
            a.doctor_id != SelectedDoctorFilter.employee_id)
            return false;

        // date (match by calendar day)
        if (FilterDate is not null)
        {
            if (a.date is null) return false;
            if (a.date.Value.Date != FilterDate.Value.Date) return false;
        }

        // status (case-insensitive exact; use codes)
        if (!string.IsNullOrWhiteSpace(FilterStatus) &&
            !string.Equals(a.status, FilterStatus, StringComparison.OrdinalIgnoreCase))
            return false;

        return true;
    }

    private bool _suppressViewRefresh;

    partial void OnSelectedDoctorFilterChanged(doctor? _)
    {
        if (!_suppressViewRefresh) AppointmentsView?.Refresh();
    }

    partial void OnFilterDateChanged(DateTime? _)
    {
        if (!_suppressViewRefresh) AppointmentsView?.Refresh();
    }

    partial void OnFilterStatusChanged(string? _)
    {
        if (!_suppressViewRefresh) AppointmentsView?.Refresh();
    }

    private async Task LoadData()
    {
        Appointments.Clear();
        var appointments=  await _appointmentService.GetAllAppointments();
        foreach (var appointment in appointments)
        {
            Console.WriteLine(appointment.date);
           Appointments.Add(appointment); 
        }
        
        Doctors.Clear();
        var doctors = await _doctorService.GetAllDoctors();
        foreach (var doctor in doctors)
        {
            Doctors.Add(doctor);
        }
        Patients.Clear();
        var patients = await _patientService.GetAllPatients();
        foreach (var patient in patients)
        {
            Patients.Add(patient);
        }
        
       AppointmentsView?.Refresh(); 
        
    }
    partial void OnSelectedAppointmentChanged(appointment? value)
    {
        if (value is not null) IsControls2Enabled = true;
        else IsControls2Enabled = false;
        if (value?.date is DateTime dt)
        {
            NewAppointmentDate = dt.Date;
            NewAppointmentTime = dt.ToString("HH:mm");
        }
        else
        {
            NewAppointmentDate = null;
            NewAppointmentTime = null;
        }
    }


    partial void OnNewAppointmentDateChanged(DateTime? _)
        => UpdateComposedDateTimeFromFields();

    partial void OnNewAppointmentTimeChanged(string? _)
        => UpdateComposedDateTimeFromFields();

    private void UpdateComposedDateTimeFromFields()
    {
        if (SelectedAppointment is null)
            return;

        // If either piece is missing or invalid, don't overwrite the entity
        if (!NewAppointmentDate.HasValue ||
            string.IsNullOrWhiteSpace(NewAppointmentTime) ||
            !TimeSpan.TryParseExact(NewAppointmentTime.Trim(), "hh\\:mm",
                CultureInfo.InvariantCulture, out var time))
        {
            return;
        }

        // Compose and push back into the entity
        SelectedAppointment.date = NewAppointmentDate.Value.Date + time;

        // Notify dependent bindings (e.g., a grid column bound to SelectedAppointment.date)
        OnPropertyChanged(nameof(SelectedAppointment));
    }
    
    
    
    public async Task ActivateAsync()
    {
        await LoadData();
    }


    
    [RelayCommand]
    private void ClearFilters()
    {
        _suppressViewRefresh = true;
        try
        {
            using (AppointmentsView.DeferRefresh())
            {
                SelectedDoctorFilter = null;
                FilterDate = null;
                FilterStatus = null;
            }
        }
        finally
        {
            _suppressViewRefresh = false;
            AppointmentsView?.Refresh(); // single refresh after defer ends
        }
    }

    [RelayCommand]
    private void EditAppointment()
    {
        IsControlsEnabled = true;
    }

    [RelayCommand]
    private async Task DeleteAppointment()
    {
        if (SelectedAppointment is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("appointmentError"));
            return;
        }
        await _appointmentService.DeleteAppointment(SelectedAppointment);
        await LoadData();
    }

    [RelayCommand]
    private void CancelAppointment()
    {
        IsControlsEnabled = false;
        SelectedDoctor = null;
        SelectedAppointment = null;
        SelectedPatient = null;
    }

    [RelayCommand]
    private async Task SaveAppointment()
    {
        if (SelectedAppointment is null) return;
        if (NewAppointmentDate is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("timeError"));
            return;
        }
        var time = TimeSpan.Zero;
        if (!string.IsNullOrWhiteSpace(NewAppointmentTime))
        {
            if (TimeSpan.TryParse(NewAppointmentTime, out var parsed))
            {
                time = parsed;
            }
        }

        var dateTime = NewAppointmentDate.Value.Date + time;
        
        SelectedAppointment.date = dateTime;
        
        await _appointmentService.UpdateAppointment(SelectedAppointment);
        await LoadData();
        IsControlsEnabled = false;

    }
}