



using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Utils;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Nurse.ViewModels;

public partial class NurseRegisterPatientViewModel : ObservableObject, IActivable
{ 
    
    public bool IsLoaded { get; private set; }
    
    
    private readonly PatientService _patientService;
    public ISnackbarMessageQueue MessageQueue { get; set; }
    private readonly LocalizationManager _localizationManager;


    private bool _isEditing = false;
    
    [ObservableProperty]
    private patient? _selectedPatient;
    [ObservableProperty] private bool _isControlsEnabled = false;
    [ObservableProperty] private bool _umcnEditable = false;

    [ObservableProperty] private ObservableCollection<patient> _patients = new();


    public NurseRegisterPatientViewModel(PatientService patientService)
    {
        _patientService = patientService;
        
        _localizationManager = App.HostApp.Services.GetRequiredService<LocalizationManager>();
        MessageQueue = new SnackbarMessageQueue();
    }


    [ObservableProperty] private bool _isControls2Enabled = false;


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


    partial void OnSelectedPatientChanged(patient? value)
    {
        if (value != null) IsControls2Enabled = true;
        else IsControls2Enabled = false;
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
        UmcnEditable = false;
    }


    [RelayCommand]
    private async Task DeletePatient()
    {
        if (SelectedPatient == null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("patientError"));
            return;
        }
        await _patientService.DeletePatient(SelectedPatient);
        await LoadData();
    }


    [RelayCommand]
    private async Task SavePatient()
    {
        if (SelectedPatient == null) return;
        Console.WriteLine("tu sam");
        if (SelectedPatient.umcn is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("patientUmcnError"));
            Console.WriteLine( _localizationManager.GetString("patientUmcnError") );
            Console.WriteLine("umcn error");
            return;
        }
        if (SelectedPatient.umcn.Length != 13)
        {
            Console.WriteLine("umcn error len");
            MessageQueue.Enqueue(_localizationManager.GetString("patientUmcnError"));
            return;
        }

        if (SelectedPatient.umcn.Any(char.IsLetter))
        {
            MessageQueue.Enqueue(_localizationManager.GetString("patientUmcnLetterError"));
            return;
        }

        if (SelectedPatient.name is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("nameError"));
            return;
        }
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