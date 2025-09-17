





using HospitalManagementSystem.Data.Models;

namespace HospitalManagementSystem;


public class LoggedInUser
{
    public employee LoggedInEmployee { get; set; }
    
    
    public event Action<employee> EmployeeChanged;


    public void SetLoggedInEmployee(employee employee)
    {
        LoggedInEmployee = employee;
        EmployeeChanged?.Invoke(employee);
    }
}