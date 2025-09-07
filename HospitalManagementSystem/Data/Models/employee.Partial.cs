using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Data.Models;

public partial class employee
{
    [NotMapped]
    public string Role
    {
        get
        {
            if (doctor != null) return "Doctor";
            if (nurse != null) return "Nurse";
            if (surgeon != null) return "Surgeon";
            if (laboratory_tehnician != null) return "Lab Technician";
            // If you have a navigation to administrator, include it:
            if (administrator != null) return "Administrator";
            return "Employee";
        }
    }

    [NotMapped]
    public string Department =>
        doctor?.specialty ?? string.Empty; // extend if other roles gain departments later
}