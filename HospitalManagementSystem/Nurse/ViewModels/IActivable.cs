
namespace HospitalManagementSystem.Nurse.ViewModels;

public interface IActivable
{
    bool IsLoaded { get; }
    Task ActivateAsync();
}