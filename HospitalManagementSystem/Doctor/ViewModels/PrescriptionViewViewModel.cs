




using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Utils;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Doctor.ViewModels;



public partial class PrescriptionViewViewModel : ObservableObject, IActivable
{
    
    private readonly PatientService _patientService;
    private SharedDataService _sharedDataService;



    public ISnackbarMessageQueue MessageQueue { get; set; }
    private readonly LocalizationManager _localizationManager;

    public ObservableCollection<string> Medications { get; } = new ObservableCollection<string>
    {
        "Paracetamol",
        "Ibuprofen",
        "Amoxicillin",
        "Metformin",
        "Atorvastatin",
        "Amlodipine",
        "Omeprazole",
        "Simvastatin",
        "Losartan",
        "Azithromycin",
        "Lisinopril",
        "Levothyroxine",
        "Metoprolol",
        "Ciprofloxacin",
        "Prednisone",
        "Hydrochlorothiazide",
        "Gabapentin",
        "Sertraline",
        "Furosemide",
        "Pantoprazole",
        "Alprazolam",
        "Clopidogrel",
        "Escitalopram",
        "Tramadol",
        "Doxycycline",
        "Warfarin",
        "Cetirizine",
        "Montelukast",
        "Tamsulosin",
        "Rosuvastatin",
        "Aspirin",
        "Insulin",
        "Salbutamol",
        "Ranitidine",
        "Fluoxetine",
        "Enalapril",
        "Bisoprolol",
        "Allopurinol",
        "Spironolactone",
        "Atenolol",
        "Citalopram",
        "Duloxetine",
        "Mirtazapine",
        "Loratadine",
        "Nitrofurantoin",
        "Propranolol",
        "Ramipril",
        "Sitagliptin",
        "Valsartan",
        "Venlafaxine"
    };


    [ObservableProperty] private ObservableCollection<prescription> _prescriptions = new();
    
    
    [ObservableProperty]
    private ICollectionView _prescriptionsView;

    [ObservableProperty] private string? _searchPrescription;
    [ObservableProperty]
    private DateTime? _searchPrescriptionStartDate;
    [ObservableProperty]
    private DateTime? _searchPrescriptionEndDate;
    
    public ICollectionView MedicationsView { get; }
    
    [ObservableProperty]
    private string? _selectedMedication;

     private string? _medicationSearchText;
    public string MedicationSearchText
    {
        get => _medicationSearchText;
        set
        {
            if (_medicationSearchText != value)
            {
                _medicationSearchText = value;
                OnPropertyChanged(nameof(MedicationSearchText));
                MedicationsView.Refresh();
            }
        }
    }
    
    
    [ObservableProperty]
    private prescription? _selectedPrescription;

    [ObservableProperty] private patient? _selectedPatient;


    [ObservableProperty] private bool _isControlsEnabled;
    [ObservableProperty]
    private bool _isControls2Enabled;

    private bool _isEditing;

    public PrescriptionViewViewModel(PatientService patientService, SharedDataService sharedDataService)
    {
        _patientService = patientService;
        _sharedDataService = sharedDataService;
        
        _sharedDataService.PatientChanged += OnPatientChanged;
        
        _localizationManager = App.HostApp.Services.GetRequiredService<LocalizationManager>();
        MessageQueue = new SnackbarMessageQueue();
        
        MedicationsView = CollectionViewSource.GetDefaultView(Medications);
        MedicationsView.Filter = o =>
        {
            if (string.IsNullOrEmpty(MedicationSearchText))
                return true;
            return o is string med && med.IndexOf(MedicationSearchText, StringComparison.OrdinalIgnoreCase) >= 0;
        };
        
        
        PrescriptionsView = CollectionViewSource.GetDefaultView(Prescriptions);
        PrescriptionsView.Filter = PrescriptionsFilter;

    }

    private bool PrescriptionsFilter(object? obj)
    {
       if(obj is not prescription p) return false;
       if(!string.IsNullOrWhiteSpace(SearchPrescription)) {
       {
           if (!p.medication.Contains(SearchPrescription, StringComparison.OrdinalIgnoreCase)) return false;
       }}

       if (SearchPrescriptionStartDate is not null)
       {
           if (p.date < SearchPrescriptionStartDate) return false;
       }

       if (SearchPrescriptionEndDate is not null)
       {
           if (p.date > SearchPrescriptionEndDate) return false;
       }

       return true;
    }


    partial void OnSearchPrescriptionChanged(string? value)
    {
        PrescriptionsView.Refresh();
    }


    partial void OnSearchPrescriptionStartDateChanged(DateTime? value)
    {
        PrescriptionsView.Refresh();
    }

    partial void OnSearchPrescriptionEndDateChanged(DateTime? value)
    {
        PrescriptionsView.Refresh();
    }


    [RelayCommand]
    private void ClearFilters()
    {
        SearchPrescription = null;
        SearchPrescriptionStartDate = null;
        SearchPrescriptionEndDate = null;
    }
    
    
    
    private void OnPatientChanged(patient patient)
    {
        SelectedPatient = _sharedDataService.CurrentPatient;
    }



    [RelayCommand]
    private void NewPrescription()
    {
        IsControlsEnabled = true;
        SelectedPrescription = new();
    }
    
    
    
    public async Task ActivateAsync()
    {

        await LoadData();
    }

    partial void OnSelectedPrescriptionChanged(prescription? prescription)
    {
        if(prescription is null) return;
        IsControls2Enabled = true;
        SelectedMedication = null;
        SelectedMedication =  prescription.medication;
    }

    private async Task LoadData()
    {
        if (SelectedPatient is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("patientError"));
            return;
        }
        Prescriptions.Clear();
        var prescriptions = await _patientService.GetPrescriptions(SelectedPatient);
        foreach (var prescription in prescriptions)
        {
            Prescriptions.Add(prescription);
        }
        
    }

    [RelayCommand]
    private void EditPrescription()
    {
        IsControlsEnabled = true;
        _isEditing = true;
    }

    [RelayCommand]
    private void CancelPrescription()
    {
        IsControlsEnabled = false;
        SelectedPrescription = null;
        SelectedMedication = null;
    }

    [RelayCommand]
    private async Task SavePrescription()
    {
        if (SelectedPrescription is null)
        {
            
            MessageQueue.Enqueue(_localizationManager.GetString("prescriptionError"));
            return;
        }

        if (SelectedPrescription.date is null)
        {
            
            MessageQueue.Enqueue(_localizationManager.GetString("dateError"));
            return;
        }
        if (SelectedMedication is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("medicationError"));
            return;
        };
        if (SelectedPatient is null)
        {
            MessageQueue.Enqueue(_localizationManager.GetString("patientError"));
            return;
        }

        SelectedPrescription.patient_umcn = SelectedPatient.umcn;
        SelectedPrescription.medication = SelectedMedication;
        if (_isEditing)
        {
            await _patientService.UpdatePrescription(SelectedPrescription);
        }
        else
        {
            await _patientService.SavePrescription(SelectedPrescription);
        }
        
        await LoadData();
        _isEditing = false;
        IsControlsEnabled = false;
        IsControls2Enabled = false;
        SelectedPrescription = null;
        SelectedMedication = null;
    }
}