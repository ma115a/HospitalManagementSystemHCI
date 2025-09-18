using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("driver")]
public partial class driver
{
    [Key]
    public int driver_id { get; set; }

    [StringLength(20)]
    public string? name { get; set; }

    [StringLength(20)]
    public string? lastname { get; set; }

    [StringLength(20)]
    public string? phone { get; set; }

    [StringLength(100)]
    public string? address { get; set; }

    public DateOnly? date_employed { get; set; }
}
