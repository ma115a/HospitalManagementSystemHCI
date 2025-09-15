
namespace HospitalManagementSystem.Utils;

public interface IActivable
{
    // bool IsLoaded { get; }
    Task ActivateAsync();
}