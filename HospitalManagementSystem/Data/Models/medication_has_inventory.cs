using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Keyless]
[Table("medication_has_inventory")]
[Index("inventory_inventory_id", Name = "fk_medication_has_inventory_inventory1_idx")]
[Index("medication_medication_id", Name = "fk_medication_has_inventory_medication1_idx")]
public partial class medication_has_inventory
{
    public int medication_medication_id { get; set; }

    public int inventory_inventory_id { get; set; }

    public int? quantity { get; set; }

    [ForeignKey("inventory_inventory_id")]
    public virtual inventory inventory_inventory { get; set; } = null!;

    [ForeignKey("medication_medication_id")]
    public virtual medication medication_medication { get; set; } = null!;
}
