using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("inventory")]
public partial class inventory
{
    [Key]
    public int inventory_id { get; set; }

    [StringLength(45)]
    public string? location { get; set; }
}
