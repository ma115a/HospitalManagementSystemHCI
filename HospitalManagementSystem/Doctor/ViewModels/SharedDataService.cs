




using HospitalManagementSystem.Data.Models;

namespace HospitalManagementSystem.Doctor.ViewModels;



public class SharedDataService
{
    public patient CurrentPatient { get; set; }

    public event Action<patient> PatientChanged;

    public void SetCurrentPatient(patient patient)
    {
        CurrentPatient = patient;
        PatientChanged?.Invoke(patient);
        
    }
}