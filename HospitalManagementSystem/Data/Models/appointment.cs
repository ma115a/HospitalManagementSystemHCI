using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("appointment")]
[Index("doctor_id", Name = "fk_appointment_doctor2_idx")]
[Index("patient_umcn", Name = "fk_appointment_patient2_idx")]
public partial class appointment
{
    [Key]
    public int appointment_id { get; set; }

    public int? doctor_id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? date { get; set; }

    [StringLength(13)]
    public string patient_umcn { get; set; } = null!;
    
    
    public string? notes { get; set; }
    [StringLength(45)]
    public string? status { get; set; }

    [ForeignKey("doctor_id")]
    [InverseProperty("appointments")]
    public virtual doctor? doctor { get; set; }

    [ForeignKey("patient_umcn")]
    [InverseProperty("appointments")]
    public virtual patient patient_umcnNavigation { get; set; } = null!;
}
