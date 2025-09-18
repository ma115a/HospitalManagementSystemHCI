using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("medical_record")]
[Index("doctor_id", Name = "fk_medical_record_doctor1_idx")]
[Index("patient_umcn", Name = "fk_medical_record_patient1")]
public partial class medical_record
{
    [Key]
    public int medical_record_id { get; set; }

    public int? doctor_id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? date { get; set; }

    [StringLength(200)]
    public string? diagnosis { get; set; }

    [StringLength(100)]
    public string? treatment { get; set; }
    
    [StringLength(200)]
    public string? notes { get; set; }

    [StringLength(13)]
    public string patient_umcn { get; set; } = null!;

    [ForeignKey("doctor_id")]
    [InverseProperty("medical_records")]
    public virtual doctor? doctor { get; set; }

    [ForeignKey("patient_umcn")]
    [InverseProperty("medical_records")]
    public virtual patient patient_umcnNavigation { get; set; } = null!;
}
