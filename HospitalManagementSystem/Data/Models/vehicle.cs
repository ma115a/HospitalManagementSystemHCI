using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("vehicle")]
public partial class vehicle
{
    [Key]
    public int vehicle_id { get; set; }

    [StringLength(20)]
    public string? brand { get; set; }

    [StringLength(20)]
    public string? model { get; set; }

    [StringLength(15)]
    public string? registration { get; set; }

    [StringLength(45)]
    public string? status { get; set; }

    public string? notes { get; set; }

    public DateOnly? last_service { get; set; }
}
