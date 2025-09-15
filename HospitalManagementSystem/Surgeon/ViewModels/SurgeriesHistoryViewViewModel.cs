


using CommunityToolkit.Mvvm.ComponentModel;
using HospitalManagementSystem.Utils;

namespace HospitalManagementSystem.Surgeon.ViewModels;


public partial class SurgeriesHistoryViewViewModel : ObservableObject, IActivable
{
    private readonly IServiceProvider _sp;


    public SurgeriesHistoryViewViewModel(IServiceProvider sp)
    {
        _sp = sp;
    }
    
    
    
    
    public Task ActivateAsync()
    {
        return null;
    }
}
