


using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Utils;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.LabWorker.ViewModels;


public partial class LabWorkerResultsViewModel : ObservableObject, IActivable
{
    
    public ISnackbarMessageQueue MessageQueue { get; set; }
    private readonly LocalizationManager _localizationManager;
    private LoggedInUser _user;
    [ObservableProperty] private employee _employee;
    
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
        _localizationManager = App.HostApp.Services.GetRequiredService<LocalizationManager>();
        MessageQueue = new SnackbarMessageQueue();
        _user = App.HostApp.Services.GetRequiredService<LoggedInUser>();
        Employee = _user.LoggedInEmployee;
        _user.EmployeeChanged += OnUserChanged;
    }
    
    private void OnUserChanged(employee value)
    {
        Console.WriteLine("OnUserChanged");
        Employee = _user.LoggedInEmployee;
    }


    public async Task ActivateAsync()
    {
        await LoadData();
        LaboratoryTestResult = new laboratory_test_result();
    }


    private async Task LoadData()
    {
        LaboratoryTests.Clear();
        var tests = await _laboratoryTestService.GetScheduledTestsForLabWorker(_user.LoggedInEmployee.employee_id);
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

        if (SelectedLaboratoryTest is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("laboratoryTestError"));
            return;
        }
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