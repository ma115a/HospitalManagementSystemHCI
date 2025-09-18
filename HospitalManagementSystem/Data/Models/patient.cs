using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("patient")]
public partial class patient
{
    [StringLength(100)]
    public string? name { get; set; }

    

    [Key]
    [StringLength(13)]
    public string umcn { get; set; } = null!;

    [StringLength(20)]
    public string? phone { get; set; }
    
    public string? notes { get; set; }

    [InverseProperty("patient_umcnNavigation")]
    public virtual ICollection<admission> admissions { get; set; } = new List<admission>();

    [InverseProperty("patient_umcnNavigation")]
    public virtual ICollection<appointment> appointments { get; set; } = new List<appointment>();

    [InverseProperty("patient_umcnNavigation")]
    public virtual ICollection<laboratory_test> laboratory_tests { get; set; } = new List<laboratory_test>();

    [InverseProperty("patient_umcnNavigation")]
    public virtual ICollection<medical_record> medical_records { get; set; } = new List<medical_record>();

    [InverseProperty("patient_umcnNavigation")]
    public virtual ICollection<prescription> prescriptions { get; set; } = new List<prescription>();

    [InverseProperty("patient_umcnNavigation")]
    public virtual ICollection<surgery> surgeries { get; set; } = new List<surgery>();
}
