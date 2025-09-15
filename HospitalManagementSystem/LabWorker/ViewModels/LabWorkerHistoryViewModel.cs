


using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Admin.Services;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Utils;

namespace HospitalManagementSystem.LabWorker.ViewModels;


public partial class LabWorkerHistoryViewModel : ObservableObject, IActivable
{
    
    private readonly LaboratoryTestService  _laboratoryTestService;
    private readonly UserService _userService;
    private readonly PatientService _patientService;

    
    
    
    [ObservableProperty] private ObservableCollection<doctor> _doctors = new();
    [ObservableProperty]
    private ObservableCollection<patient>  _patients = new();
    [ObservableProperty]
    private ObservableCollection<nurse>  _nurses = new();
    [ObservableProperty] private ICollectionView? _laboratoryTestsView;
    [ObservableProperty]
    private ObservableCollection<laboratory_test> _laboratoryTests = new();
    
    
    [ObservableProperty]
    private laboratory_test?  _selectedLaboratoryTest;
    

    
    
    [ObservableProperty] private doctor? _selectedDoctorFilter;
    [ObservableProperty] private nurse? _selectedNurseFilter;
    [ObservableProperty] private DateTime? _startDateFilter;
    [ObservableProperty] private DateTime? _endDateFilter;
    [ObservableProperty]
    private patient? _selectedPatientFilter;

    
    
    public LabWorkerHistoryViewModel(LaboratoryTestService laboratoryTestService, UserService userService,
        PatientService patientService)
    {
        _laboratoryTestService = laboratoryTestService;
        _userService = userService;
        _patientService = patientService;
        
        LaboratoryTestsView = CollectionViewSource.GetDefaultView(LaboratoryTests);
        LaboratoryTestsView.Filter = LaboratoryTestsFilter;
    }
    
    
    
    
    partial void OnSelectedDoctorFilterChanged(doctor? _)
    {
        LaboratoryTestsView?.Refresh();
    }
    
    partial void OnSelectedNurseFilterChanged(nurse? _)
    {
        LaboratoryTestsView?.Refresh();
    }


    partial void OnStartDateFilterChanged(DateTime? date)
    {
        Console.WriteLine(date);
        LaboratoryTestsView?.Refresh();
    }

    partial void OnSelectedPatientFilterChanged(patient? _)
    {
        LaboratoryTestsView?.Refresh();
        
    }

    partial void OnEndDateFilterChanged(DateTime? _)
    {
        LaboratoryTestsView?.Refresh();
    }

    
    [RelayCommand]
    private void ClearFilters()
    {
        StartDateFilter = null;
        EndDateFilter = null;
        SelectedDoctorFilter = null;
        SelectedNurseFilter = null;
        SelectedPatientFilter = null;
    }
    
    
    private bool LaboratoryTestsFilter(object? obj)
    {
        if (obj is not laboratory_test t) return false;
        if (SelectedDoctorFilter is not null)
        {
            if (t.doctor_id != SelectedDoctorFilter.employee_id) return false;
        }
        
        if (SelectedNurseFilter is not null)
        {
            if (t.nurse_id != SelectedNurseFilter.employee_id) return false;
        }

        if (SelectedPatientFilter is not null)
        {
            if (t.patient_umcn != SelectedPatientFilter.umcn) return false;
        }

        if (StartDateFilter is not null)
        {
            if (t.date < DateOnly.FromDateTime(StartDateFilter.Value.Date)) return false;
        }

        if (EndDateFilter is not null)
        {
            if (t.date > DateOnly.FromDateTime(EndDateFilter.Value.Date)) return false;
        }

        return true;
    }

    
    private async Task LoadData()
    {
        Doctors.Clear();
        var doctors = await _userService.GetDoctors();
        foreach (var doctor in doctors)
        {
            Doctors.Add(doctor);
        }
        Nurses.Clear();
        var nurses = await _userService.GetNurseWorkers();
        foreach (var nurse in nurses)
        {
            Nurses.Add(nurse);
        }
        
        Patients.Clear();
        var patients = await _patientService.GetAllPatients();
        foreach (var patient in patients)
        {
            Patients.Add(patient);
        }
        
        
        LaboratoryTests.Clear();
        var tests = await _laboratoryTestService.GetFinishedLaboratoryTests();
        foreach (var test in tests)
        {
            LaboratoryTests.Add(test);
            Console.WriteLine(test.laboratory_test_result.result_data);
        }

    }
    
    public async Task ActivateAsync()
    {
        await LoadData();

    }
    
}