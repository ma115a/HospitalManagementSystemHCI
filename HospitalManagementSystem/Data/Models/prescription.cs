using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("prescription")]
[Index("doctor_id", Name = "fk_prescription_doctor2_idx")]
// [Index("medication_id", Name = "fk_prescription_medication1_idx")]
[Index("patient_umcn", Name = "fk_prescription_patient2_idx")]
public partial class prescription
{
    [Key]
    public int prescription_id { get; set; }

    [StringLength(13)]
    public string? patient_umcn { get; set; }

    public int? doctor_id { get; set; }

    

    [StringLength(50)]
    public string? dosage { get; set; }

    [StringLength(50)]
    public string? frequency { get; set; }
    
    [StringLength(45)]
    public string? duration { get; set; }
    
    [StringLength(45)]
    public string? refills { get; set; }
    
    
    [Column(TypeName = "datetime")]
    public DateTime? date { get; set; }
    
    [StringLength(200)]
    public string? notes { get; set; }

    [ForeignKey("doctor_id")]
    [InverseProperty("prescriptions")]
    public virtual doctor? doctor { get; set; }
    
    
    [StringLength(100)]
    public string? medication { get; set; }

    // [ForeignKey("medication_id")]
    // [InverseProperty("prescriptions")]
    // public virtual medication? medication { get; set; }

    [ForeignKey("patient_umcn")]
    [InverseProperty("prescriptions")]
    public virtual patient? patient_umcnNavigation { get; set; }
}
