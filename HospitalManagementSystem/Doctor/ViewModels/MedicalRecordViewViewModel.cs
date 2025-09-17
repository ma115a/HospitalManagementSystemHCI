



using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Utils;

namespace HospitalManagementSystem.Doctor.ViewModels;


public partial class MedicalRecordViewViewModel : ObservableObject, IActivable
{
    private SharedDataService _sharedDataService;
    private readonly PatientService _patientService;


    [ObservableProperty] private patient? _selectedPatient;

    [ObservableProperty] private medical_record? _selectedMedicalRecord;
    [ObservableProperty] private DateTime? _selectedDate;

    [ObservableProperty] private string? _selectedTime;


    [ObservableProperty] private bool _isControlsEnabled;
    [ObservableProperty] private bool _isControls2Enabled;
    private bool _isEditing;


    [ObservableProperty] private ObservableCollection<medical_record> _medicalRecords = new();
    [ObservableProperty] private ICollectionView  _medicalRecordsView;

    [ObservableProperty] private string? _recordSearch;
    [ObservableProperty] private DateTime? _startDateFilter;
    [ObservableProperty] private DateTime? _endDateFilter;
    






    public MedicalRecordViewViewModel(SharedDataService sharedDataService, PatientService patientService)
    {
        _sharedDataService = sharedDataService;
        _patientService = patientService;

        _sharedDataService.PatientChanged += OnPatientChanged;


        MedicalRecordsView = CollectionViewSource.GetDefaultView(MedicalRecords);
        MedicalRecordsView.Filter = MedicalRecordsFilter;
    }



    partial void OnSelectedMedicalRecordChanged(medical_record? record)
    {
        if (record?.date is DateTime dt)
        {
            SelectedDate = dt.Date;
            SelectedTime = dt.ToString("HH:mm");
        }
        else
        {
            SelectedDate = null;
            SelectedTime = null;
        }

        if (record is not null) IsControls2Enabled = true;
    }


    private bool MedicalRecordsFilter(object? obj)
    {
        if (obj is not medical_record record) return false;
        if (!string.IsNullOrWhiteSpace(RecordSearch))
        {
            if (!record.diagnosis.Contains(RecordSearch, StringComparison.OrdinalIgnoreCase)) return false;
        }

        if (StartDateFilter is not null)
        {
            if (record.date < StartDateFilter) return false;
        }

        if (EndDateFilter is not null)
        {
            if (record.date > EndDateFilter) return false;
        }

        return true;
    }

    partial void OnRecordSearchChanged(string? value)
    {
        MedicalRecordsView?.Refresh();
    }

    partial void OnStartDateFilterChanged(DateTime? value)
    {
        MedicalRecordsView?.Refresh();
    }

    partial void OnEndDateFilterChanged(DateTime? value)
    {
        MedicalRecordsView?.Refresh();
    }


    [RelayCommand]
    private void ClearFilters()
    {
        RecordSearch = null;
        StartDateFilter = null;
        EndDateFilter = null;
    }

    public async Task ActivateAsync()
    {

        SelectedTime = null;
        SelectedDate = null;
        await LoadData();
    }


    private async Task LoadData()
    {
        if (SelectedPatient is null) return;
        MedicalRecords.Clear();
        var records = await _patientService.GetMedicalRecords(SelectedPatient);
        foreach (var record in records)
        {
            MedicalRecords.Add(record);
        }
        

    }

    

    private void OnPatientChanged(patient patient)
    {
        SelectedPatient = _sharedDataService.CurrentPatient;
    }


    [RelayCommand]
    private void NewMedicalRecord()
    {
        if (SelectedPatient is null) return;
        IsControlsEnabled = true;
        SelectedMedicalRecord = new();
    }
    
    
    [RelayCommand]
    private void EditMedicalRecord()
    {
        _isEditing = true;
        IsControlsEnabled = true;

    }



    [RelayCommand]
    private async Task DeleteMedicalRecord()
    {

        if (SelectedMedicalRecord is null) return;
        await _patientService.DeleteMedicalRecord(SelectedMedicalRecord);
        await LoadData();
    }


    [RelayCommand]
    private async Task SaveMedicalRecord()
    {
        if (SelectedMedicalRecord is null) return;
        if (SelectedDate is null) return;
        if (SelectedTime is null) return;
        if (SelectedPatient is null) return;
        var time = TimeSpan.Zero;
        if (!string.IsNullOrWhiteSpace(SelectedTime))
        {
            if (TimeSpan.TryParse(SelectedTime, out var parsed))
            {
                time = parsed;
            }
        }

        var dateTime = SelectedDate.Value.Date + time;
        SelectedMedicalRecord.date = dateTime;
        SelectedMedicalRecord.patient_umcn = SelectedPatient.umcn;
        if (_isEditing)
        {

            await _patientService.UpdateMedicalRecord(SelectedMedicalRecord);
        }
        else
        {
            await _patientService.SaveMedicalRecord(SelectedMedicalRecord);
        }
        await LoadData();
        _isEditing = false;
        IsControlsEnabled = false;
    }



}