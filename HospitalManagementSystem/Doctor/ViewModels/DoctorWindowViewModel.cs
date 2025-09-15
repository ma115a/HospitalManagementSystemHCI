


using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HOspitalManagementSystem.Doctor.ViewModels;
using HospitalManagementSystem.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Doctor.ViewModels;


public partial class DoctorWindowViewModel : ObservableObject
{

    private readonly IServiceProvider _sp;
    private readonly Dictionary<int, Lazy<IActivable>> _slides;


    public DoctorWindowViewModel(IServiceProvider sp)
    {
        _sp = sp;
        _slides = new Dictionary<int, Lazy<IActivable>>()
        {

            { 0, new Lazy<IActivable>(() => _sp.GetRequiredService<DoctorHomePageViewModel>()) },
            { 1, new Lazy<IActivable>(() => _sp.GetRequiredService<MedicalRecordViewViewModel>()) },
            { 2, new Lazy<IActivable>(() => _sp.GetRequiredService<PrescriptionViewViewModel>()) },
            { 3, new Lazy<IActivable>(() => _sp.GetRequiredService<LaboratoryResultsViewViewModel>()) },
            { 4, new Lazy<IActivable>(() => _sp.GetRequiredService<LaboratoryRequestsViewViewModel>()) },
            { 5, new Lazy<IActivable>(() => _sp.GetRequiredService<MakeAdmissionViewViewModel>()) },
            { 6, new Lazy<IActivable>(() => _sp.GetRequiredService<SurgeriesViewViewModel>()) }
        };

    }


    public DoctorHomePageViewModel? HomeVm =>
        _slides.TryGetValue(0, out var l) ? (DoctorHomePageViewModel?)l.Value : null;
    public MedicalRecordViewViewModel? MedicalVm =>
        _slides.TryGetValue(2, out var l) ? (MedicalRecordViewViewModel?)l.Value : null;
    public PrescriptionViewViewModel? PrescriptionVm =>
        _slides.TryGetValue(2, out var l) ? (PrescriptionViewViewModel?)l.Value : null;
    public LaboratoryResultsViewViewModel? LabResultVm =>
        _slides.TryGetValue(3, out var l) ? (LaboratoryResultsViewViewModel?)l.Value : null;
    public LaboratoryRequestsViewViewModel? LabRequestVm =>
        _slides.TryGetValue(4, out var l) ? (LaboratoryRequestsViewViewModel?)l.Value : null;
    public MakeAdmissionViewViewModel? AdmissionVm =>
        _slides.TryGetValue(5, out var l) ? (MakeAdmissionViewViewModel?)l.Value : null;
    public SurgeriesViewViewModel? SurgeriesVm =>
        _slides.TryGetValue(6, out var l) ? (SurgeriesViewViewModel?)l.Value : null;
    



    [ObservableProperty] private int _currentSlideIndex = 0;



    partial void OnCurrentSlideIndexChanged(int value)
    {
        _ = ActivateSelectedAsync(value);
    }
    
    private async Task ActivateSelectedAsync(int index)
    {
        if (_slides.TryGetValue(index, out var lazy))
        {
            var vm = lazy.Value;
            await vm.ActivateAsync();
        }
    }
    
    
    [RelayCommand]
    private void GoToDashboard()
    {
        CurrentSlideIndex = 0;
    }


    [RelayCommand]
    private void GoToMedicalRecord()
    {
        CurrentSlideIndex = 1;
    }

    [RelayCommand]
    private void GoToPrescription()
    {
        CurrentSlideIndex = 2;
    }

    [RelayCommand]
    private void GoToLaboratoryResult()
    {
        CurrentSlideIndex = 3;
    }

    [RelayCommand]
    private void GoToLaboratoryRequests()
    {
        CurrentSlideIndex = 4;
    }

    [RelayCommand]
    private void GoToAdmission()
    {

        CurrentSlideIndex = 5;
    }


    [RelayCommand]
    private void GoToSurgeries()
    {
        CurrentSlideIndex = 6;
    }
    





}