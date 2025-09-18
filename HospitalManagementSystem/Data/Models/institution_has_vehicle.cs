using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Keyless]
[Table("institution_has_vehicle")]
[Index("institution_institution_id", Name = "fk_institution_has_vehicle_institution1_idx")]
[Index("vehicle_vehicle_id", Name = "fk_institution_has_vehicle_vehicle1_idx")]
public partial class institution_has_vehicle
{
    public int institution_institution_id { get; set; }

    public int vehicle_vehicle_id { get; set; }

    [ForeignKey("institution_institution_id")]
    public virtual institution institution_institution { get; set; } = null!;

    [ForeignKey("vehicle_vehicle_id")]
    public virtual vehicle vehicle_vehicle { get; set; } = null!;
}
