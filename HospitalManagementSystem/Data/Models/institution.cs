using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("institution")]
public partial class institution
{
    [Key]
    public int institution_id { get; set; }

    [StringLength(45)]
    public string name { get; set; } = null!;

    [StringLength(100)]
    public string address { get; set; } = null!;

    [StringLength(20)]
    public string phone { get; set; } = null!;
}
