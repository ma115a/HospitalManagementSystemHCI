



using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Admin.Services;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Utils;

namespace HospitalManagementSystem.Doctor.ViewModels;



public partial class LaboratoryRequestsViewViewModel : ObservableObject, IActivable
{

    private SharedDataService _sharedDataService;
    private readonly LaboratoryTestService _laboratoryTestService;
    private readonly PatientService _patientService;
    private readonly UserService  _userService;

    
    [ObservableProperty]
    private ObservableCollection<laboratory_test> _laboratoryTests = new ();
    
    [ObservableProperty]
    private ObservableCollection<laboratory_test_result>  _laboratoryTestResults = new ();
    
    
    
    [ObservableProperty]
    private ObservableCollection<laboratory_tehnician>  _technicians = new ();
    
    [ObservableProperty]
    private patient? _selectedPatient;
    [ObservableProperty]
    private laboratory_test? _selectedLaboratoryTest;
    
    [ObservableProperty]
    private laboratory_test_result? _selectedLaboratoryTestResult;


    [ObservableProperty] private laboratory_tehnician? _selectedLaboratoryTechnician;


    [ObservableProperty] private ICollectionView _testsView;
    
    [ObservableProperty]
    private ICollectionView _resultsView;
    
    [ObservableProperty] private int _selectedTabIndex = 0;
    
    private bool _isEditing = false;
    
    [ObservableProperty]
    private bool _isOptionsEnabled = true;
    [ObservableProperty] private bool _isControlsEnabled = false;


    [ObservableProperty] private string? _searchText;
    [ObservableProperty] private DateTime? _searchStartDate;
    [ObservableProperty] private DateTime? _searchEndDate;
    
    [ObservableProperty]
    private string? _searchStatus;

    [ObservableProperty] private bool _isStatusEnabled;
    
    partial void OnSelectedTabIndexChanged(int idx)
    {
        IsOptionsEnabled = idx == 0;
        IsStatusEnabled = idx == 0;
    }
    public LaboratoryRequestsViewViewModel(SharedDataService sharedDataService, LaboratoryTestService laboratoryTestService, PatientService patientService,  UserService userService)
    {
        _sharedDataService = sharedDataService;
        _laboratoryTestService = laboratoryTestService;
        _patientService = patientService;
        _userService = userService;
        _sharedDataService.PatientChanged += OnPatientChanged;



        TestsView = CollectionViewSource.GetDefaultView(LaboratoryTests);
        TestsView.Filter = LaboratoryTestFilter;

        ResultsView = CollectionViewSource.GetDefaultView(LaboratoryTestResults);
        ResultsView.Filter = LaboratoryResultFilter;

    }
    private void OnPatientChanged(patient patient)
    {
        SelectedPatient = _sharedDataService.CurrentPatient;
    }


    private bool LaboratoryTestFilter(object? obj)
    {
        if (obj is not laboratory_test t) return false;
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            if (!t.name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) return false;
        }

        if (SearchStartDate is not null)
        {
            if (t.date < DateOnly.FromDateTime(SearchStartDate.Value)) return false;
        }

        if (SearchEndDate is not null)
        {
            if (t.date > DateOnly.FromDateTime(SearchEndDate.Value)) return false;
        }

        if (!string.IsNullOrWhiteSpace(SearchStatus))
        {
            if (!t.status.Equals(SearchStatus)) return false;
        }
        

        return true;
    }


    private bool LaboratoryResultFilter(object? obj)
    {
        if (obj is not laboratory_test_result r) return false;
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            if(!r.laboratory_test.name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) return false;
        }
        
        if (SearchStartDate is not null)
        {
            if (r.laboratory_test.date < DateOnly.FromDateTime(SearchStartDate.Value)) return false;
        }

        if (SearchEndDate is not null)
        {
            if (r.laboratory_test.date > DateOnly.FromDateTime(SearchEndDate.Value)) return false;
        }

        return true;
    }

    partial void OnSearchTextChanged(string? value)
    {
        if (SelectedTabIndex == 0)
        {
            TestsView.Refresh();
        } else ResultsView.Refresh();
    }

    partial void OnSearchStartDateChanged(DateTime? value)
    {
        if (SelectedTabIndex == 0)
        {
            Console.WriteLine("index 0");
            TestsView.Refresh();
        } else ResultsView.Refresh();
    }

    partial void OnSearchEndDateChanged(DateTime? value)
    {
        if (SelectedTabIndex == 0)
        {
            TestsView.Refresh();
        } else ResultsView.Refresh();
    }

    partial void OnSearchStatusChanged(string? value)
    {
        Console.WriteLine(value);
        TestsView.Refresh();
    }
    
    


    public async Task ActivateAsync()
    {
        await LoadData();
        await LoadTests();
        await LoadResults();
    }
    
    private async Task LoadData()
    {
        Technicians.Clear();
        var technicians = await _userService.GetLabWorkers();
        foreach (var technician in technicians)
        {
            Technicians.Add(technician);
        }
        
        
    }
    
    private async Task LoadTests()
    {

        if (SelectedPatient is null) return;
        LaboratoryTests.Clear();
        var laboratoryTests = await _laboratoryTestService.GetAllTestsForPatient(SelectedPatient);
        foreach (var laboratoryTest in laboratoryTests)
        {
            LaboratoryTests.Add(laboratoryTest);
        }
    }
    
    private async Task LoadResults()
    {
        if (SelectedPatient is null) return;
        var results = await _laboratoryTestService.GetAllTestResultsForPatient(SelectedPatient);
        foreach (var result in results)
        {
            LaboratoryTestResults.Add(result);
        }
    }


    [RelayCommand]
    private void ClearFilters()
    {
        SearchText = null;
        SearchStartDate = null;
        SearchEndDate = null;
        SearchStatus = null;
    }
    
    
    
    [RelayCommand]
        private void  EditTest()
    {
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
        if (SelectedPatient is null) return;
        if (SelectedLaboratoryTest is null) return;
        if(SelectedLaboratoryTechnician is null) return;

        if (_isEditing)
        {
            await _laboratoryTestService.EditTest(SelectedLaboratoryTest, SelectedPatient, SelectedLaboratoryTechnician);
            _isEditing = false;
        }
        else
        {
            await _laboratoryTestService.SaveTest(SelectedLaboratoryTest, SelectedPatient, SelectedLaboratoryTechnician);
        }


        await LoadTests();
        SelectedLaboratoryTest = null;
        SelectedPatient = null;
    }
    

    
    
}