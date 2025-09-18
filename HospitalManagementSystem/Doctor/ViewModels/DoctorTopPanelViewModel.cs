




using CommunityToolkit.Mvvm.ComponentModel;
using HospitalManagementSystem.Data.Models;

namespace HospitalManagementSystem.Doctor.ViewModels;



public partial class DoctorTopPanelViewModel : ObservableObject
{
    
    
    
    
    private readonly LoggedInUser _user;


    [ObservableProperty] private employee _employee;


    public DoctorTopPanelViewModel(LoggedInUser user)
    {
        _user = user;
        Employee =  _user.LoggedInEmployee;
        _user.EmployeeChanged += OnUserChanged;
        
    }


    public void OnUserChanged(employee value)
    {
        Employee = _user.LoggedInEmployee;
    }
    
    
    
}