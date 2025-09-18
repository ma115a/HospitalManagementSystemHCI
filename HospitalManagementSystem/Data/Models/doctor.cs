using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("doctor")]
public partial class doctor
{
    [Key]
    public int employee_id { get; set; }

    [StringLength(45)]
    public string specialty { get; set; } = null!;

    [InverseProperty("doctor")]
    public virtual ICollection<appointment> appointments { get; set; } = new List<appointment>();

    [InverseProperty("doctor_employee")]
    public virtual ICollection<department> departments { get; set; } = new List<department>();

    [ForeignKey("employee_id")]
    [InverseProperty("doctor")]
    public virtual employee employee { get; set; } = null!;

    [InverseProperty("doctor")]
    public virtual ICollection<laboratory_test> laboratory_tests { get; set; } = new List<laboratory_test>();

    [InverseProperty("doctor")]
    public virtual ICollection<medical_record> medical_records { get; set; } = new List<medical_record>();

    [InverseProperty("doctor")]
    public virtual ICollection<prescription> prescriptions { get; set; } = new List<prescription>();
}
