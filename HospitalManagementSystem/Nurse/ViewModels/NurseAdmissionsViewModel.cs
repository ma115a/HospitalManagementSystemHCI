using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Admin.Services;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Utils;

namespace HospitalManagementSystem.Nurse.ViewModels;


public partial class NurseAdmissionsViewModel : ObservableObject, IActivable
{
    private readonly DepartmentService _departmentService;
    private readonly PatientService _patientService;
    private readonly AdmissionService _admissionService;

    [ObservableProperty] private ObservableCollection<department> _departments = new();
    [ObservableProperty]
    private ObservableCollection<patient>  _patients = new();

    [ObservableProperty] private ICollectionView _roomsView;
    [ObservableProperty] private department? _filterDepartment;

    [ObservableProperty]
    private ObservableCollection<room> _rooms = new();
    
    [ObservableProperty]
    private ObservableCollection<admission> _admissions = new ();
    
    [ObservableProperty]
    private room? _selectedRoom;

    [ObservableProperty] private bool _isControlsEnabled;
    
    [ObservableProperty]
    private admission? _selectedAdmission;
    
    [ObservableProperty]
    private patient? _selectedPatient;

    private bool _isEditing;
    


    public NurseAdmissionsViewModel(DepartmentService departmentService, PatientService patientService, AdmissionService admissionService)
    {
        _departmentService = departmentService;
        _patientService = patientService;
        _admissionService = admissionService;
        
        
        RoomsView = CollectionViewSource.GetDefaultView(Rooms);
        RoomsView.Filter = RoomsFilter;
    }


    private bool _suppressViewRefresh;

    private bool RoomsFilter(object? obj)
    {
        if (obj is not room r) return false;
        if(FilterDepartment is not null && r.department_id != FilterDepartment.department_id) return false;
        return true;
    }


    partial void OnFilterDepartmentChanged(department? _)
    {
        if(!_suppressViewRefresh) RoomsView?.Refresh();
    }


    partial void OnSelectedRoomChanged(room? room)
    {
        if (room is null) return;
        if (!_isEditing) LoadAdmissionsCommand.Execute(null);
        
    }
    
    
    
    
    
    
    
   public bool IsLoaded { get; private set; }
    public async Task ActivateAsync()
    {
        Console.WriteLine("NurseAdmissionsViewModel");
        await LoadData();
    }

    [RelayCommand]
    private async Task LoadAdmissions()
    {
        if (SelectedRoom is null) return;
        Admissions.Clear();
        var admissions = await _admissionService.GetAllAdmissionsForRoom(SelectedRoom);
        foreach (var admission in admissions)
        {
            Admissions.Add(admission);
        }

    }


    private async Task LoadData()
    {
        Console.WriteLine("begin");
        Departments.Clear(); 
        var departments = await _departmentService.GetAllDepartments();
        foreach (var department in departments)
        {
            Departments.Add(department);    
        }

        Rooms.Clear();
        var rooms = await _departmentService.GetAllRooms();
        foreach (var room in rooms)
        {
            Rooms.Add(room);
        }
        Patients.Clear();
        var patients =  await _patientService.GetAllPatients();
        foreach (var patient in patients)
        {
            Patients.Add(patient);
        }
        Console.WriteLine("done");
        RoomsView?.Refresh();
    }


    [RelayCommand]
    private void NewAdmission()
    {
        IsControlsEnabled = true;
        SelectedAdmission = new admission();
    }
    
   [RelayCommand] 
   private void EditAdmission()
   {
       IsControlsEnabled = true;

       _isEditing = true;
   }


   [RelayCommand]
   private async Task DeleteAdmission()
   {
       if (SelectedAdmission is null) return;
       await _admissionService.DeleteAdmission(SelectedAdmission);
       await LoadAdmissions();
   }
    

    [RelayCommand]
    private void CancelAdmission()
    {
        IsControlsEnabled = false;
        SelectedAdmission = null;
        SelectedPatient = null;
        _isEditing = false;
    }

    [RelayCommand]
    private async Task SaveAdmission()
    {
        if (SelectedRoom is null) return;
        if (SelectedPatient is null) return;
        if (SelectedAdmission is null) return;
        if (_isEditing)
        {
            await _admissionService.UpdateAdmission(SelectedAdmission, SelectedRoom, SelectedPatient);
            _isEditing = false;
        }
        else
        {
            await _admissionService.SaveAdmission(SelectedAdmission, SelectedRoom, SelectedPatient);
        }

        IsControlsEnabled = false;
        SelectedAdmission = null;
        SelectedPatient = null;
        SelectedRoom = null;
        await LoadAdmissions();
    }





    [RelayCommand]
    private void ClearFilter()
    {
       _suppressViewRefresh = true;
       try
       {
           using (RoomsView.DeferRefresh())
           {
               FilterDepartment = null;
           }
       }
       finally
       {
           _suppressViewRefresh = false;
           RoomsView?.Refresh();
           ;
       }
    }
}