


using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Utils;

namespace HospitalManagementSystem.Doctor.ViewModels;


public partial class DoctorHomePageViewModel : ObservableObject, IActivable 
{
    
    
    private readonly PatientService _patientService;
    SharedDataService _sharedDataService;


    [ObservableProperty] private ObservableCollection<patient> _patients = new();
    [ObservableProperty]
    private patient? _selectedPatient;
    
    
    
    public DoctorHomePageViewModel(PatientService patientService, SharedDataService sharedDataService)
    {
        _patientService = patientService;
        _sharedDataService = sharedDataService;
        
    }


    partial void OnSelectedPatientChanged(patient? value)
    {
        if (value is null) return;
        _sharedDataService.SetCurrentPatient(value);
    }


    public async Task ActivateAsync()
    {
        await LoadData();
    }


    private async Task LoadData()
    {
        Patients.Clear();
        var patients = await _patientService.GetAllPatients();
        foreach (var patient in patients)
        {
            Patients.Add(patient);
        }

    }
    
}