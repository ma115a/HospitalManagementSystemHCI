using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("department")]
[Index("doctor_employee_id", Name = "fk_department_doctor1_idx")]
public partial class department
{
    [Key]
    public int department_id { get; set; }

    [StringLength(45)]
    public string? name { get; set; }

    public int doctor_employee_id { get; set; }

    [StringLength(45)]
    public string? code { get; set; }
    
    public bool? surgery_department { get; set; }

    [ForeignKey("doctor_employee_id")]
    [InverseProperty("departments")]
    public virtual doctor doctor_employee { get; set; } = null!;

    [InverseProperty("department")]
    public virtual ICollection<room> rooms { get; set; } = new List<room>();
}
