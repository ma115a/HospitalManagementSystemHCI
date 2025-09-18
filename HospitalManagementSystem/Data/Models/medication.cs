using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("medication")]
public partial class medication
{
    [Key]
    public int medication_id { get; set; }

    [StringLength(45)]
    public string? name { get; set; }

    // [InverseProperty("medication")]
    // public virtual ICollection<prescription> prescriptions { get; set; } = new List<prescription>();
}
