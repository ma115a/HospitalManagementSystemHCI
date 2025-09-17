



using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Admin.Services;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Utils;
using HospitalManagementSystem.Surgeon.ViewModels;

namespace HospitalManagementSystem.Doctor.ViewModels;




public partial class DoctorSurgeriesViewViewModel : ObservableObject, IActivable
{

    private readonly PatientService _patientService;
    private readonly DepartmentService _departmentService;
    private readonly SurgeryService  _surgeryService;
    private readonly UserService _userService;
    private SharedDataService _sharedDataService;

    [ObservableProperty] private ObservableCollection<patient> _patients = new();
    
    [ObservableProperty] private ObservableCollection<room> _rooms = new();
    
    [ObservableProperty]
    private ObservableCollection<NurseItem> _nurses = new();
    
    [ObservableProperty]
    private ObservableCollection<NurseItem> _selectedNurses = new();
    
    [ObservableProperty]
    private ObservableCollection<surgeon>  _surgeons = new();
    [ObservableProperty]
    private surgeon? _selectedSurgeon;


    [ObservableProperty] private ObservableCollection<surgery> _surgeries = new();
    
    [ObservableProperty]
    private ICollectionView _surgeriesView;
    
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
    [ObservableProperty] private bool _isNursePickerOpen;

    [ObservableProperty] private string? _searchText;
    [ObservableProperty]
    private DateTime? _searchStartDate;
    [ObservableProperty]
    private DateTime? _searchEndDate;

    [ObservableProperty] private string? _searchStatus;

    [ObservableProperty] private bool _isControlsEnabled;


    public DoctorSurgeriesViewViewModel(PatientService patientService, DepartmentService departmentService,
        SurgeryService surgeryService, UserService userService, SharedDataService sharedDataService)
    {
        _patientService = patientService;
        _sharedDataService = sharedDataService;
        _departmentService = departmentService;
        _surgeryService = surgeryService;
        _userService = userService;
        
        _sharedDataService.PatientChanged += OnPatientChanged;
        
        SurgeriesView = CollectionViewSource.GetDefaultView(Surgeries);
        SurgeriesView.Filter = SurgeriesFilter;
    }
    
    private void OnPatientChanged(patient patient)
    {
        SelectedPatient = _sharedDataService.CurrentPatient;
    }


    private bool SurgeriesFilter(object? obj)
    {
        if (obj is not surgery s) return false;
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            if (!s.procedure.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)) return false;
        }

        if (SearchStartDate is not null)
        {
            if (s.date < SearchStartDate) return false;
        }
        if (SearchEndDate is not null)
        {
            if (s.date > SearchEndDate) return false;
        }

        if (!string.IsNullOrWhiteSpace(SearchStatus))
        {
            if (!s.status.Equals(SearchStatus)) return false;
        }
        return true;
    }


    partial void OnSearchTextChanged(string? value)
    {
        Console.WriteLine(value);
        SurgeriesView.Refresh();
        
    }
    
    partial void OnSearchEndDateChanged(DateTime? value)
    {
        SurgeriesView.Refresh();
        
    }
    partial void OnSearchStartDateChanged(DateTime? value)
    {
        SurgeriesView.Refresh();
        
    }
    partial void OnSearchStatusChanged(string? value)
    {
        SurgeriesView.Refresh();
        
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
    private void ScheduleSurgery()
    {
        SelectedSurgery = new();
        IsControlsEnabled = true;
    }
    
    
    
    
    

    
    public async Task ActivateAsync()
    {
        SelectedSurgery = new surgery();
        await LoadData();
        await LoadSurgeries();

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
    }   
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
        Surgeons.Clear();
        var surgeons = await _userService.GetSurgeons();
        foreach (var surgeon in surgeons)
        {
            Surgeons.Add(surgeon);
        }

    }

    private async Task LoadSurgeries()
    {
        if (SelectedPatient is null) return;
        Surgeries.Clear();
        var surgeries = await _surgeryService.GetSurgeriesForPatient(SelectedPatient);
        foreach (var surgery in surgeries)
        {
            Surgeries.Add(surgery);
            Console.WriteLine(surgery.procedure);
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
        Console.WriteLine("broj sestara" + nurses.Count());
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
        await LoadSurgeries();


        SelectedSurgery = null;
        SelectedPatient = null;
        SelectedRoom = null;
        SelectedSurgeryDate = null;
        SelectedSurgeryTime = null;
        SelectedNurses = null;
        _nurseSelectionSnapshot = null;
        SelectedSurgeon = null;
        IsControlsEnabled = false;

    }
}