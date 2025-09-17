


using CommunityToolkit.Mvvm.ComponentModel;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Utils;

namespace HospitalManagementSystem.Nurse.ViewModels;

public partial class NurseSurgeriesViewModel : ObservableObject, IActivable
{
     
    private readonly SurgeryService _surgeryService;


    public NurseSurgeriesViewModel(SurgeryService surgeryService)
    {
        _surgeryService = surgeryService;
    }
    public async Task ActivateAsync()
    {

    }



    private async Task LoadData()
    {
        
    }
    
}