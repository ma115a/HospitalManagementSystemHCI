



using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Admin.Services;
using HospitalManagementSystem.Data.Models;

public partial class DepartmentRoomViewModel : ObservableObject
{

    private readonly DepartmentService _departmentService;
    [ObservableProperty] private bool _isNewDepartmentOpen = false;
    [ObservableProperty] private bool _isNewRoomOpen = false;

    public bool IsAnyDialogOpen
    {
        get => IsNewDepartmentOpen || IsNewRoomOpen;
        set
        {
            if (!value)
            {
                IsNewDepartmentOpen = false;
                IsNewRoomOpen = false;
                OnPropertyChanged(nameof(IsAnyDialogOpen));
            }
        }
    }
    

    [ObservableProperty] private ObservableCollection<doctor> _doctors = new();
    [ObservableProperty]
    private doctor _selectedHeadDoctor;
    
    [ObservableProperty]
    private ObservableCollection<department> _departments = new();
    
    [ObservableProperty]
    private ObservableCollection<room> _rooms = new();

    [ObservableProperty] private department? _selectedDepartment;
    
    [ObservableProperty]
    private room? _selectedRoom;


    public DepartmentRoomViewModel(DepartmentService service)
    {
        _departmentService = service;
        _ = LoadData();

    }

    partial void OnIsNewDepartmentOpenChanged(bool value)
    {
        OnPropertyChanged(nameof(IsAnyDialogOpen));
    }
    partial void OnIsNewRoomOpenChanged(bool value)
    {
        OnPropertyChanged(nameof(IsAnyDialogOpen));
    }


    private async Task LoadData()
    {
        Doctors.Clear();
        var doctors =  await _departmentService.GetAllDoctors();
        foreach (var doctor in doctors)
        {
            Doctors.Add(doctor);
        }
        Departments.Clear();
        var departments = await _departmentService.GetAllDepartments();
        foreach (var department in departments)
        {
            Console.WriteLine(department.name);
            Departments.Add(department);
        }
    }
    

    [RelayCommand]
    private void ClearDepartmentSelection()
    {
        SelectedDepartment = null;
    }

    [RelayCommand]
    private void ClearRoomSelection()
    {
        SelectedRoom = null;
    }


    partial  void OnSelectedDepartmentChanged(department? value)
    {
        if (value == null) return;
        var rooms =  _departmentService.GetRoomsForDepartment(value);
        Rooms.Clear();
        foreach (var room in rooms)
        {
            Rooms.Add(room);
        }

    }


    [RelayCommand]
    private void NewDepartment()
    {
        IsNewRoomOpen = false;
        IsNewDepartmentOpen = true;
        SelectedDepartment = new department
        {
            name = string.Empty,
            code = string.Empty
        };
        OnPropertyChanged(nameof(IsAnyDialogOpen));
    }

    [RelayCommand]
    private void NewRoom()
    {
        SelectedDepartment ??= SelectedDepartment ?? Departments.FirstOrDefault();
        IsNewDepartmentOpen = false;
        IsNewRoomOpen = true;
        OnPropertyChanged(nameof(IsAnyDialogOpen));
        SelectedRoom = new room();
    }


    [RelayCommand]
    private void CloseNewDepartment()
    {
        IsNewDepartmentOpen = false;
        SelectedDepartment = null;
    }

    [RelayCommand]
    private void CloseNewRoom()
    {
        IsNewRoomOpen = false;
        SelectedRoom = null;
    }


    [RelayCommand]
    private async Task Delete()
    {
        if (SelectedDepartment != null && SelectedRoom == null)
        {
           await _departmentService.DeleteDepartment(SelectedDepartment);
           Departments.Clear();
           var departments = await _departmentService.GetAllDepartments();
           foreach (var department in departments)
           {
               Console.WriteLine(department.name);
               Departments.Add(department);
           }
            
        } else if (SelectedDepartment != null && SelectedRoom != null)
        {
            await _departmentService.DeleteRoom(SelectedRoom);
            var rooms =  _departmentService.GetRoomsForDepartment(SelectedDepartment);
            Rooms.Clear();
            foreach (var room in rooms)
            {
                Rooms.Add(room);
            }
        }
    }


    [RelayCommand]
    private async Task SaveRoom()
    {
        if (SelectedRoom == null) return;
        if (SelectedDepartment == null) return;

        try
        {

            await _departmentService.CreateRoom(SelectedRoom, SelectedDepartment);
            IsNewRoomOpen = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        await LoadData();
    }

    [RelayCommand]
    private async Task SaveDepartment()
    {
        if (SelectedDepartment == null) return;
        Console.WriteLine("nije null");
        try
        {
            await _departmentService.CreateDepartment(SelectedDepartment, SelectedHeadDoctor);
            IsNewDepartmentOpen = false;
            
            await LoadData();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

}