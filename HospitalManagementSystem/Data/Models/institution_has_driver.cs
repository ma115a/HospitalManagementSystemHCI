using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Keyless]
[Table("institution_has_driver")]
[Index("driver_driver_id", Name = "fk_institution_has_driver_driver1_idx")]
[Index("institution_institution_id", Name = "fk_institution_has_driver_institution1_idx")]
public partial class institution_has_driver
{
    public int institution_institution_id { get; set; }

    public int driver_driver_id { get; set; }

    [ForeignKey("driver_driver_id")]
    public virtual driver driver_driver { get; set; } = null!;

    [ForeignKey("institution_institution_id")]
    public virtual institution institution_institution { get; set; } = null!;
}
