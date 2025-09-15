
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Admin.Services;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Utils;

namespace HospitalManagementSystem.Surgeon.ViewModels;


public partial class NurseItem : ObservableObject
{
    public int Id { get; }
    public string FullName { get; }

    [ObservableProperty] private bool isSelected;  

    public NurseItem(int id, string fullName)
    {
        Id = id; FullName = fullName;
    }
}


public partial class ScheduleSurgeryViewModel : ObservableObject, IActivable
{
    private readonly IServiceProvider _sp;
    
    private readonly PatientService _patientService;
    private readonly DepartmentService _departmentService;
    private readonly SurgeryService  _surgeryService;
    private readonly UserService _userService;

    [ObservableProperty] private ObservableCollection<patient> _patients = new();
    
    [ObservableProperty] private ObservableCollection<room> _rooms = new();
    
    [ObservableProperty]
    private ObservableCollection<NurseItem> _nurses = new();
    [ObservableProperty]
    private ObservableCollection<NurseItem> _selectedNurses = new();

    public ICollectionView NursesView { get; private set; }
    private HashSet<int> _nurseSelectionSnapshot = new();
    private IEnumerable<int> CurrentSelectedIds()
        => Nurses.Where(n => n.IsSelected).Select(n => n.Id);
    
    [ObservableProperty]
    private patient? _selectedPatient;
    
    [ObservableProperty]
    private room? _selectedRoom;
    
    [ObservableProperty]
    private surgery? _selectedSurgery;

    [ObservableProperty] private DateTime? _selectedSurgeryDate;
    [ObservableProperty] private string? _selectedSurgeryTime;
    [ObservableProperty] private int? _selectedSurgeryDuration;


    [ObservableProperty] private string nurseFilterText;
    [ObservableProperty] private bool isNursePickerOpen;



    public ScheduleSurgeryViewModel(IServiceProvider sp, PatientService patientService,  DepartmentService departmentService, SurgeryService surgeryService, UserService userService)
    {
        _sp = sp;
        _patientService = patientService;
        _departmentService = departmentService;
        _surgeryService = surgeryService;
        _userService = userService;
    }
    
    
    
    public async Task ActivateAsync()
    {
        SelectedSurgery = new surgery();
        await LoadData();

        NursesView = CollectionViewSource.GetDefaultView(Nurses);
        NursesView.Filter = o =>
        {
            if (o is not NurseItem n) return false;
            if (string.IsNullOrWhiteSpace(NurseFilterText)) return true;
            return n.FullName.IndexOf(NurseFilterText, StringComparison.InvariantCultureIgnoreCase) >= 0;
        };
        OnPropertyChanged(nameof(NursesView)); // <-- notify binding

        // attach to existing items too (not only future CollectionChanged additions)
        foreach (var n in Nurses) n.PropertyChanged += Nurse_PropertyChanged;

        Nurses.CollectionChanged += (_, __) =>
        {
            foreach (var n in Nurses) n.PropertyChanged -= Nurse_PropertyChanged; // avoid dupes
            foreach (var n in Nurses) n.PropertyChanged += Nurse_PropertyChanged;
        };
    }   // }


    partial void OnNurseFilterTextChanged(string value) => NursesView.Refresh();

    private void Nurse_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(NurseItem.IsSelected)) return;
        var n = (NurseItem)sender!;
        if (n.IsSelected && !SelectedNurses.Contains(n)) SelectedNurses.Add(n);
        if (!n.IsSelected && SelectedNurses.Contains(n)) SelectedNurses.Remove(n);
    }


    [RelayCommand]
    private void OpenNursePicker()
    {
        // take snapshot before any user changes
        _nurseSelectionSnapshot = CurrentSelectedIds().ToHashSet();
        IsNursePickerOpen = true;
    }

    [RelayCommand]
    private void CancelNursePicker()
    {
        // restore original selection
        var keep = _nurseSelectionSnapshot;

        // Temporarily disable filter refresh spam (optional)
        // if you see perf issues when restoring a lot of rows.
        foreach (var n in Nurses)
            n.IsSelected = keep.Contains(n.Id);

        IsNursePickerOpen = false;
    }
    
    [RelayCommand]
    private void ApplyNursePicker()
    {
        // accept current selection; snapshot no longer needed
        _nurseSelectionSnapshot.Clear();
        IsNursePickerOpen = false;
    }

    [RelayCommand]
    public void RemoveNurse(NurseItem nurse)
    {
        nurse.IsSelected = false; // automatically prunes from SelectedNurses
    }


    [RelayCommand]
    partial void OnSelectedSurgeryDurationChanged(int? duration)
    {
        Console.WriteLine("Duration changed");
        if(SelectedSurgeryDate is null) return;
        if (SelectedSurgeryTime is null) return;
        Console.WriteLine("Duration changed not null");
        _ = LoadAvailableNurses();

    }



    private async Task LoadData()
    {
        var patients = await _patientService.GetAllPatients();
        Patients.Clear();
        foreach (var patient in patients)
        {
            Patients.Add(patient);
        }
        var rooms = await _departmentService.GetSurgeryRooms();
        Rooms.Clear();
        foreach (var room in rooms)
        {
            Console.WriteLine(room.number);
            Rooms.Add(room);
        }
    }


    private async Task LoadAvailableNurses()
    {
        if(SelectedSurgery is null) return;
        if (SelectedSurgeryDate is null) return;
        Nurses.Clear();
        var time = TimeSpan.Zero; 
        if (!string.IsNullOrWhiteSpace(SelectedSurgeryTime))
        {
            if (TimeSpan.TryParse(SelectedSurgeryTime, out var parsed))
            {
                time = parsed;
            }
        }

        DateTime? surgeryDateTime = SelectedSurgeryDate.Value.Date + time;
        var nurses = await _surgeryService.GetAvailableNursesForSurgery(surgeryDateTime, SelectedSurgeryDuration);
        foreach (var nurse in nurses) Nurses.Add(new NurseItem(nurse.employee_id, nurse.employee.name));
    }



    [RelayCommand]
    private async Task SaveSurgery()
    {
        if (SelectedSurgery is null) return;
        if (SelectedPatient is null) return;
        if (SelectedRoom is null) return;
        var time = TimeSpan.Zero;
        if (!string.IsNullOrWhiteSpace(SelectedSurgeryTime))
        {
            if (TimeSpan.TryParse(SelectedSurgeryTime, out var parsed))
            {
                time = parsed;
            }
        }

        SelectedSurgery.duration = SelectedSurgeryDuration;
        SelectedSurgery.date = SelectedSurgeryDate.Value.Date + time;
        var end_time = TimeSpan.FromMinutes((double)SelectedSurgery.duration);
        SelectedSurgery.end_date = SelectedSurgery.date.Value + end_time;
        await _surgeryService.SaveSurgery(SelectedSurgery, SelectedPatient, SelectedRoom, SelectedNurses);


        SelectedSurgery = null;
        SelectedPatient = null;
        SelectedRoom = null;
        SelectedSurgeryDate = null;
        SelectedSurgeryTime = null;
        SelectedNurses = null;
        _nurseSelectionSnapshot = null;

        foreach (var nurse in NursesView)
        {
        }
    }
}