
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Admin.Services;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Utils;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Nurse.ViewModels;


public partial class NurseLabViewModel : ObservableObject, IActivable
{
    private readonly LaboratoryTestService _laboratoryTestService;
    private readonly PatientService _patientService;
    private readonly UserService  _userService;
    private readonly LoggedInUser _user;
    public ISnackbarMessageQueue MessageQueue { get; set; }
    private readonly LocalizationManager _localizationManager;
    
    
    [ObservableProperty]
    private ObservableCollection<patient>  _patients = new();
    
    [ObservableProperty]
    private ObservableCollection<laboratory_tehnician>  _technicians = new ();
    
    
    [ObservableProperty]
    private ObservableCollection<laboratory_test> _laboratoryTests = new ();
    // Requested / Ordered → doctor or nurse created the request.
    //
    //     Scheduled → assigned to a lab technician or time slot.
    //
    //     In Progress → sample taken, analysis running.
    //
    //     Awaiting Results → test finished, waiting for technician to enter/validate results.
    //
    //     Completed → results are entered.
    //
    //     Reviewed / Verified → doctor has checked/approved.
    //
    //     Cancelled → test cancelled before execution.
    //
    //     Rejected / Failed → test could not be processed (bad sample, technical issue).
    
    
    [ObservableProperty]
    private ObservableCollection<laboratory_test_result>  _laboratoryTestResults = new ();

    [ObservableProperty]
    private patient? _selectedPatient;
    [ObservableProperty]
    private laboratory_test? _selectedLaboratoryTest;
    
    [ObservableProperty]
    private laboratory_test_result? _selectedLaboratoryTestResult;
    
    [ObservableProperty]
    private laboratory_tehnician? _selectedLaboratoryTechnician;


    [ObservableProperty] private patient? _selectedPatientFilter;


    [ObservableProperty] private patient? _selectedPatientFilterResult;


    [ObservableProperty] private ICollectionView _testsView;
    
    [ObservableProperty]
    private ICollectionView _resultsView;


    private bool _isEditing = false;


    [ObservableProperty] private int _selectedTabIndex = 0;
    
    [ObservableProperty]
    private bool _isOptionsEnabled = true;

    partial void OnSelectedTabIndexChanged(int idx)
    {
        IsOptionsEnabled = idx == 0;
    }






    public NurseLabViewModel(LaboratoryTestService laboratoryTestService, PatientService patientService, UserService userService)
    {
        _laboratoryTestService = laboratoryTestService;
        _patientService = patientService;
        _userService = userService;
        _user = App.HostApp.Services.GetRequiredService<LoggedInUser>();
        
        
        _localizationManager = App.HostApp.Services.GetRequiredService<LocalizationManager>();
        MessageQueue = new SnackbarMessageQueue();
        TestsView = CollectionViewSource.GetDefaultView(LaboratoryTests);
        TestsView.Filter = TestsFilter;

        ResultsView = CollectionViewSource.GetDefaultView(LaboratoryTestResults);
        ResultsView.Filter = ResultsFilter;
    }
   
   public bool IsLoaded { get; private set; }
    public async Task ActivateAsync()
    {

        await LoadData();
        await LoadTests();
        await LoadResults();
    }


    private bool TestsFilter(object? obj)
    {
        if (obj is not laboratory_test t) return false;
        if (SelectedPatientFilter is not null && t.patient_umcn != SelectedPatientFilter.umcn) return false;
        return true;
    }

    private bool ResultsFilter(object? obj)
    {
        if (obj is not laboratory_test_result r) return false;
        if (SelectedPatientFilterResult is not null &&
            r.laboratory_test.patient_umcn != SelectedPatientFilterResult.umcn) return false;
        return true;
    }



    [ObservableProperty] private bool _isControlsEnabled = false;

    partial void OnSelectedPatientFilterChanged(patient? patient)
    {
        TestsView?.Refresh();
    }


    partial void OnSelectedPatientFilterResultChanged(patient? patient)
    {
        ResultsView?.Refresh();
    }


    private async Task LoadData()
    {
        Patients.Clear();
        var patients = await _patientService.GetAllPatients();
        foreach (var patient in patients)
        {
            Patients.Add(patient);
        }
        Technicians.Clear();
        var technicians = await _userService.GetLabWorkers();
        foreach (var technician in technicians)
        {
           Technicians.Add(technician);
        }
        
        
    }

    private async Task LoadTests()
    {
        
        LaboratoryTests.Clear();
        var laboratoryTests = await _laboratoryTestService.GetAllTests();
        foreach (var laboratoryTest in laboratoryTests)
        {
            LaboratoryTests.Add(laboratoryTest);
        }
    }

    private async Task LoadResults()
    {
        var results = await _laboratoryTestService.GetAllTestResults();
        foreach (var result in results)
        {
            LaboratoryTestResults.Add(result);
        }
    }


    [RelayCommand]
    private void ClearFilter()
    {
        SelectedPatientFilter = null;
    }


    [RelayCommand]
    private void ClearFilterResults()
    {
        SelectedPatientFilterResult = null;
    }

    [RelayCommand]
    private async Task DeleteLabTest()
    {
        if (SelectedLaboratoryTest is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("laboratoryTestError"));
            return;
        }
        await _laboratoryTestService.DeleteTest(SelectedLaboratoryTest);
        await LoadTests();
    }


    [RelayCommand]
    private void  EditTest()
    {   
        if (SelectedLaboratoryTest is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("laboratoryTestError"));
            return;
        }
        _isEditing = true;
        IsControlsEnabled = true;
    }

    
    [RelayCommand]
    private void NewTest()
    {
        IsControlsEnabled = true;
        SelectedLaboratoryTest = new laboratory_test();
        SelectedLaboratoryTest.status = "Requested";

    }


    [RelayCommand]
    private void CancelTest()
    {
        IsControlsEnabled = false;
        SelectedLaboratoryTest = null;
        SelectedPatient = null;
    }


    [RelayCommand]
    private async Task SaveTest()
    {
        if (SelectedPatient is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("patientError"));
            return;
        }

        if (SelectedLaboratoryTest is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("laboratoryTestError"));
            return;
        }

        if (SelectedLaboratoryTechnician is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("laboratoryTechnicianError"));
            return;
        }

        if (_isEditing)
        {
           await _laboratoryTestService.EditTest(SelectedLaboratoryTest, SelectedPatient, SelectedLaboratoryTechnician);
           _isEditing = false;
        }
        else
        {
            await _laboratoryTestService.SaveTest(SelectedLaboratoryTest, SelectedPatient, SelectedLaboratoryTechnician, _user.LoggedInEmployee.employee_id);
        }


        await LoadTests();
        SelectedLaboratoryTest = null;
        SelectedPatient = null;
        IsControlsEnabled = false;
    }
    
    
    
    
    

}