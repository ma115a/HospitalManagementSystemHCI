



using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Admin.Services;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Utils;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Surgeon.ViewModels;


public partial class SurgeriesViewViewModel : ObservableObject, IActivable
{
    private readonly IServiceProvider _sp;


    private readonly UserService _userService;
    private readonly PatientService _patientService;
    private readonly DepartmentService _departmentService;
    private readonly SurgeryService _surgeryService;
    
    public ISnackbarMessageQueue MessageQueue { get; set; }
    private readonly LocalizationManager _localizationManager;


    [ObservableProperty] private ObservableCollection<patient> _patients = new();
    [ObservableProperty] private ObservableCollection<surgery> _surgeries = new();
    [ObservableProperty] private ObservableCollection<nurse> _nurses = new();
    [ObservableProperty] private ObservableCollection<room> _rooms = new();
    
    
    [ObservableProperty]
    private ObservableCollection<nurse> _assignedNurses = new();


    [ObservableProperty] private DateTime? _surgeryDate;
    [ObservableProperty] private string? _surgeryTime;


    [ObservableProperty] private surgery? _selectedSurgery;


    [ObservableProperty] private bool _isControlsEnabled = false;
    [ObservableProperty] private bool _isOptionsEnabled = false;
    
    
    [ObservableProperty]
    private bool _isAssignNursesEnabled = false;

    [ObservableProperty] private bool _isRemoveNursesEnabled = false;



    [ObservableProperty] private ICollectionView? _surgeriesView;
    [ObservableProperty] private DateTime? _surgeryFilterDate;
    [ObservableProperty] private string? _surgeryFilterStatus;


    [ObservableProperty] private int _selectedTabIndex = 0;
    
    [ObservableProperty]
    private nurse? _selectedNurseToAdd;
    
    [ObservableProperty]
    private nurse? _selectedNurseToRemove;

    private bool _isEditing = false;



    public SurgeriesViewViewModel(IServiceProvider sp, UserService userService, PatientService patientService,
        DepartmentService departmentService, SurgeryService surgeryService)
    {
        _sp = sp;
        _userService = userService;
        _patientService = patientService;
        _departmentService = departmentService;
        _surgeryService = surgeryService;

        _localizationManager = App.HostApp.Services.GetRequiredService<LocalizationManager>();
        MessageQueue = new SnackbarMessageQueue();

        SurgeriesView = CollectionViewSource.GetDefaultView(Surgeries);
        SurgeriesView.Filter = SurgeriesFilter;
    }


    private bool SurgeriesFilter(object? obj)
    {
        if (obj is not surgery s) return false;
        if (SurgeryFilterDate is not null)
        {
            if (s.date.Value.Date != SurgeryFilterDate.Value.Date) return false;
        }

        if (!string.IsNullOrWhiteSpace(SurgeryFilterStatus) &&
            !string.Equals(s.status, SurgeryFilterStatus, StringComparison.OrdinalIgnoreCase)) return false;
        return true;
    }


    partial void OnSurgeryFilterDateChanged(DateTime? _)
    {
        SurgeriesView?.Refresh();
    }


    partial void OnSurgeryFilterStatusChanged(string? _)
    {
        SurgeriesView?.Refresh();
    }
    
    
    partial void OnSelectedNurseToAddChanged(nurse? value)
    {
        if (value is not null) IsAssignNursesEnabled = true;
    }

    partial void OnSelectedNurseToRemoveChanged(nurse? value)
    {
        if (value is not null) IsRemoveNursesEnabled = true;
    }


    [RelayCommand]
    private void ClearFilters()
    {
        SurgeryFilterDate = null;
        SurgeryFilterStatus = null;
    }



    partial void OnSelectedSurgeryChanged(surgery? value)
    {
        if (value is not null) IsOptionsEnabled = true;
        else IsOptionsEnabled = false;
        if (value is not null) IsOptionsEnabled = true;
        if (value?.date is DateTime dt)
        {
            SurgeryDate = dt.Date;
            SurgeryTime = dt.ToString("HH:mm");
        }
        else
        {
            SurgeryDate = null;
            SurgeryTime = null;
        }

        if (value?.nurses != null)
        {
            AssignedNurses = new ObservableCollection<nurse>(value.nurses);
        }
        else
        {
            AssignedNurses = new();
        }
        _ = LoadNurses();
        Console.WriteLine("surgeery changed");
    }


    public async Task ActivateAsync()
    {
        await LoadData();
        SelectedSurgery = null;
        Console.WriteLine("surgery view activated");
    }


    private async Task LoadData()
    {
        Patients.Clear();
        var patients = await _patientService.GetAllPatients();
        foreach (var patient in patients) Patients.Add(patient);

        await LoadSurgeries();

        Rooms.Clear();
        var rooms = await _departmentService.GetSurgeryRooms();
        foreach (var room in rooms)
        {
            Rooms.Add(room);
        }
    }

    private async Task LoadSurgeries()
    {
        Surgeries.Clear();
        var surgeries = await _surgeryService.GetAllSurgeries();
        foreach (var surgery in surgeries)
        {
            Surgeries.Add(surgery);
        }
    }


    private async Task LoadNurses()
    {
        if (SelectedSurgery is null) return;
        Nurses.Clear();
        var nurses = await _surgeryService.GetAvailableNurses(SelectedSurgery);
        foreach(var nurse in nurses)
        {
            Nurses.Add(nurse);
        }
    }

    partial void OnSelectedTabIndexChanged(int tabIndex)
    {
        if (tabIndex == 1)
        {
            _ = LoadNurses();
        }
        
    }


    [RelayCommand]
    private async Task AssignNurse()
    {
        if (SelectedNurseToAdd is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("nurseError"));
            return;
        }

        if (SelectedSurgery is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("surgeryError"));
            return;
        }
        Console.WriteLine($"Before: AssignedNurses count = {AssignedNurses.Count}");
        
        await _surgeryService.AssignNurseToSurgery(SelectedSurgery, SelectedNurseToAdd);
        var nurseToRemove = Nurses.FirstOrDefault(n => n.employee_id == SelectedNurseToAdd.employee_id);
        if (nurseToRemove is null) return;
        Nurses.Remove(nurseToRemove);
        SelectedSurgery.nurses.Add(SelectedNurseToAdd);
        AssignedNurses.Add(nurseToRemove);
        SelectedNurseToAdd = null;
        
        OnPropertyChanged(nameof(AssignedNurses));
        IsAssignNursesEnabled = false;

    }

    [RelayCommand]
    private async Task RemoveAssignedNurse()
    {
        if (SelectedNurseToRemove is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("nurseError"));
            return;
        }

        if (SelectedSurgery is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("surgeryError"));
            return;
        }
       await _surgeryService.RemoveNurseFromSurgery(SelectedSurgery, SelectedNurseToRemove);
       
       var nurseToRemove =
           AssignedNurses.FirstOrDefault(n => n.employee_id == SelectedNurseToRemove.employee_id);
       
       if (nurseToRemove is null) return;
       SelectedSurgery.nurses.Remove(nurseToRemove);
       AssignedNurses.Remove(nurseToRemove);
       Nurses.Add(nurseToRemove);
       SelectedNurseToRemove = null;
       IsRemoveNursesEnabled = false;
    }


    [RelayCommand]
    private void EditSurgery()
    {
        _isEditing = true;
        IsControlsEnabled = true;
    }

    [RelayCommand]
    private async Task DeleteSurgery()
    {
        if (SelectedSurgery is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("surgeryError"));
            return;
        }
        await _surgeryService.DeleteSurgery(SelectedSurgery);
        await LoadSurgeries();

        IsOptionsEnabled = false;

    }

    [RelayCommand]
    private async Task SaveSurgery()
    {
        if (SelectedSurgery is null) return;
        if (SelectedSurgery.date is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("dateError"));
            return;
        }

        var time = TimeSpan.Zero;
        if (!string.IsNullOrWhiteSpace(SurgeryTime))
        {
            if (TimeSpan.TryParse(SurgeryTime, out var parsed))
            {
                time = parsed;
            }
        }
        SelectedSurgery.date = SurgeryDate.Value.Date + time;
        SelectedSurgery.end_date = SelectedSurgery.date.Value + TimeSpan.FromMinutes((double)SelectedSurgery.duration);
        await _surgeryService.UpdateSurgery(SelectedSurgery);
        _isEditing = false;
        IsControlsEnabled = false;
         await LoadSurgeries();
    }


    [RelayCommand]
    private void Cancel()
    {
        SelectedSurgery = null;
        SurgeryTime = null;
        SurgeryDate = null;
        IsControlsEnabled = false;
        IsOptionsEnabled = false;

    }
    

}