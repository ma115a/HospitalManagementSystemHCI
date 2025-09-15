


using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Surgeon.ViewModels;



public partial class SurgeonWindowViewModel : ObservableObject
{
    private readonly IServiceProvider _sp;
    
    private readonly Dictionary<int, Lazy<IActivable>> _slides;
    
    [ObservableProperty]
    private int _currentSlideIndex;


    public SurgeonWindowViewModel(IServiceProvider sp)
    {
        _sp = sp;
        _slides = new()
        {
            { 1, new Lazy<IActivable>(() => _sp.GetRequiredService<SurgeriesViewViewModel>()) },
            { 2, new Lazy<IActivable>(() => _sp.GetRequiredService<ScheduleSurgeryViewModel>()) },
            { 3, new Lazy<IActivable>(() => _sp.GetRequiredService<SurgeriesHistoryViewViewModel>()) }
        };
    }
    
    
    public SurgeriesViewViewModel? SurgeriesVm => _slides.TryGetValue(1, out var l) ? (SurgeriesViewViewModel)l.Value : null;
    public ScheduleSurgeryViewModel? ScheduleSurgeryVm => _slides.TryGetValue(2, out var l) ? (ScheduleSurgeryViewModel)l.Value : null;
    public SurgeriesHistoryViewViewModel? HistoryVm => _slides.TryGetValue(3, out var l) ? (SurgeriesHistoryViewViewModel)l.Value : null;



    partial void OnCurrentSlideIndexChanged(int value)
    {
        _ = ActivateSelectedAsync(value);
    }

    [RelayCommand]
    private void GoToDashboard()
    {
        CurrentSlideIndex = 0;
    }


    [RelayCommand]
    private void GoToSurgeries()
    {
        CurrentSlideIndex = 1;
    }


    [RelayCommand]
    private void GoToScheduleSurgery()
    {
        CurrentSlideIndex = 2;
    }

    [RelayCommand]
    private void GoToHistory()
    {
        CurrentSlideIndex = 3;
    }
    private async Task ActivateSelectedAsync(int index)
    {
        if (_slides.TryGetValue(index, out var lazy))
        {
            var vm = lazy.Value;
            await vm.ActivateAsync();
        }
    }
}