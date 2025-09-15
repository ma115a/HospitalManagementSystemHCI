


using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Utils;

namespace HospitalManagementSystem.LabWorker.ViewModels;


public partial class LabWorkerResultsViewModel : ObservableObject, IActivable
{
    
    
    private readonly LaboratoryTestService _laboratoryTestService;
    [ObservableProperty]
    private ObservableCollection<laboratory_test> _laboratoryTests = new();
    [ObservableProperty]
    private laboratory_test? _selectedLaboratoryTest;

    [ObservableProperty]
    private DateTime? _selectedLaboratoryTestDate;

    [ObservableProperty] private string? _selectedLaboratoryTestTime;
    [ObservableProperty]
    private laboratory_test_result? _laboratoryTestResult;

    [ObservableProperty] private bool _isControlsEnabled = false;

    public LabWorkerResultsViewModel(LaboratoryTestService laboratoryTestService)
    {
        _laboratoryTestService = laboratoryTestService;
    }


    public async Task ActivateAsync()
    {
        await LoadData();
        LaboratoryTestResult = new laboratory_test_result();
    }


    private async Task LoadData()
    {
        LaboratoryTests.Clear();
        var tests = await _laboratoryTestService.GetScheduledTests();
        foreach (var test in tests)
        {
            LaboratoryTests.Add(test);
        }
    }


    partial void OnSelectedLaboratoryTestChanged(laboratory_test? value)
    {
        if (value is null) return;
        LaboratoryTestResult = new();
        IsControlsEnabled = true;

    }

    [RelayCommand]
    private void CancelResult()
    {
        IsControlsEnabled = false;
        SelectedLaboratoryTest = null;
        LaboratoryTestResult = null;
        SelectedLaboratoryTestDate = null;
        SelectedLaboratoryTestTime = null;
    }




    [RelayCommand]
    private async Task SaveResult()
    {

        if (SelectedLaboratoryTest is null) return;
        var time = TimeSpan.Zero;
        if (!string.IsNullOrWhiteSpace(SelectedLaboratoryTestTime))
        {
            if (TimeSpan.TryParse(SelectedLaboratoryTestTime, out var parsed))
            {
                time = parsed;
            }
        }

        var dateTime = SelectedLaboratoryTestDate.Value.Date + time;
        
        LaboratoryTestResult.date= dateTime;


        await _laboratoryTestService.SaveTestResult(SelectedLaboratoryTest, LaboratoryTestResult);
        await LoadData();
        SelectedLaboratoryTest = null;
        LaboratoryTestResult = null;
        SelectedLaboratoryTestDate = null;
        SelectedLaboratoryTestTime = null;
        IsControlsEnabled = false;

    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}