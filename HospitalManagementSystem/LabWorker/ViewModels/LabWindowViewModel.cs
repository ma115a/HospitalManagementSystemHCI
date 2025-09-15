

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.LabWorker.ViewModels;



public partial class LabWindowViewModel : ObservableObject
{
   private readonly IServiceProvider _sp;
   private readonly Dictionary<int, Lazy<IActivable>> _slides;


   public LabWindowViewModel(IServiceProvider sp)
   {
      _sp = sp;

      _slides = new Dictionary<int, Lazy<IActivable>>()
      {
         { 1, new Lazy<IActivable>(() => _sp.GetRequiredService<LabWorkerRequestsViewModel>()) },
         { 2, new Lazy<IActivable>(() => _sp.GetRequiredService<LabWorkerResultsViewModel>()) },
         { 3, new Lazy<IActivable>(() => _sp.GetRequiredService<LabWorkerHistoryViewModel>()) }
      };
   }
   
   
   
   public LabWorkerRequestsViewModel? RequestsVm => _slides.TryGetValue(1, out var l) ? (LabWorkerRequestsViewModel?)l.Value : null;
   public LabWorkerResultsViewModel? ResultsVm => _slides.TryGetValue(2, out var l) ? (LabWorkerResultsViewModel?)l.Value : null;
   public LabWorkerHistoryViewModel? HistoryVm => _slides.TryGetValue(3, out var l) ? (LabWorkerHistoryViewModel?)l.Value : null;




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
   private void GoToRequests()
   {
      CurrentSlideIndex = 1;
   }
   
   [RelayCommand]
   private void GoToResults()
   {
      CurrentSlideIndex = 2;
   }
   
   [RelayCommand]
   private void GoToHistory()
   {
      CurrentSlideIndex = 3;
   }

   
}