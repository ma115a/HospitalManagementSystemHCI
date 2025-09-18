using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Keyless]
[Table("surgery_has_nurse")]
[Index("nurse_nurse_id", Name = "fk_surgery_has_nurse_nurse1_idx")]
[Index("surgery_surgery_id", Name = "fk_surgery_has_nurse_surgery1_idx")]
public partial class surgery_has_nurse
{
    public int surgery_surgery_id { get; set; }

    public int nurse_nurse_id { get; set; }

    [ForeignKey("nurse_nurse_id")]
    public virtual nurse nurse_nurse { get; set; } = null!;

    [ForeignKey("surgery_surgery_id")]
    public virtual surgery surgery_surgery { get; set; } = null!;
}
