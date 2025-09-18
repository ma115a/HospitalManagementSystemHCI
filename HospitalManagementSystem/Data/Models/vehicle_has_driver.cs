using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Keyless]
[Table("vehicle_has_driver")]
[Index("driver_driver_id", Name = "fk_vehicle_has_driver1_driver1_idx")]
[Index("vehicle_vehicle_id", Name = "fk_vehicle_has_driver1_vehicle1_idx")]
public partial class vehicle_has_driver
{
    public int vehicle_vehicle_id { get; set; }

    public int driver_driver_id { get; set; }

    [ForeignKey("driver_driver_id")]
    public virtual driver driver_driver { get; set; } = null!;

    [ForeignKey("vehicle_vehicle_id")]
    public virtual vehicle vehicle_vehicle { get; set; } = null!;
}
