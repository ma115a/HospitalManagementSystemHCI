



using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Utils;

namespace HospitalManagementSystem.Nurse.ViewModels;

public partial class NurseRegisterPatientViewModel : ObservableObject, IActivable
{ 
    
    public bool IsLoaded { get; private set; }
    
    
    private readonly PatientService _patientService;


    private bool _isEditing = false;
    
    [ObservableProperty]
    private patient? _selectedPatient;
    [ObservableProperty] private bool _isControlsEnabled = false;
    [ObservableProperty] private bool _umcnEditable = false;

    [ObservableProperty] private ObservableCollection<patient> _patients = new();


    public NurseRegisterPatientViewModel(PatientService patientService)
    {
        _patientService = patientService;
    }


    private async Task LoadData()
    {
        var patients = await _patientService.GetAllPatients();
        Patients.Clear();
        foreach (var patient in patients)
        {
            Patients.Add(patient);
        }
    }
    public async Task ActivateAsync()
    {
        await LoadData();
    }



    [RelayCommand]
    private void NewPatient()
    {
        IsControlsEnabled = true;
        UmcnEditable = true;
        SelectedPatient = new patient();
    }

    [RelayCommand]
    private void EditPatient()
    {
        IsControlsEnabled = true;
        UmcnEditable = false;
        _isEditing = true;
    }


    [RelayCommand]
    private void Cancel()
    {
        IsControlsEnabled = false;
        SelectedPatient = null;
    }


    [RelayCommand]
    private async Task DeletePatient()
    {
        if (SelectedPatient == null) return;
        await _patientService.DeletePatient(SelectedPatient);
        await LoadData();
    }


    [RelayCommand]
    private async Task SavePatient()
    {
        if (SelectedPatient == null) return;
        if (_isEditing)
        {
            await _patientService.UpdatePatient(SelectedPatient);
            IsControlsEnabled = false;
            UmcnEditable = false;
        }
        else
        {
            await _patientService.CreatePatient(SelectedPatient);
            IsControlsEnabled = false;
            UmcnEditable = false;
            SelectedPatient = null;
        }

        await LoadData();


    }
    
}