


using CommunityToolkit.Mvvm.ComponentModel;

namespace HospitalManagementSystem.Nurse.ViewModels;

public partial class NurseSurgeriesViewModel : ObservableObject, IActivable
{
     
     public bool IsLoaded { get; private set; }
    public async Task ActivateAsync()
    {

    }
    
}