
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Services;

namespace HospitalManagementSystem.Nurse.ViewModels;


public partial class NurseLabViewModel : ObservableObject, IActivable
{
    private readonly LaboratoryTestService _laboratoryTestService;
    private readonly PatientService _patientService;
    
    
    [ObservableProperty]
    private ObservableCollection<patient>  _patients;

    [ObservableProperty]
    private patient? _selectedPatient;






    public NurseLabViewModel(LaboratoryTestService laboratoryTestService, PatientService patientService)
    {
        _laboratoryTestService = laboratoryTestService;
        _patientService = patientService;
    }
   
   public bool IsLoaded { get; private set; }
    public async Task ActivateAsync()
    {

    }



    [ObservableProperty] private bool _isControlsEnabled = false;


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