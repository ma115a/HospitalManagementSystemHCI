using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("admission")]
[Index("patient_umcn", Name = "fk_admission_patient2_idx")]
[Index("room_id", Name = "fk_admission_room2_idx")]
public partial class admission
{
    [Key]
    public int admission_id { get; set; }

    [Column(TypeName = "date")]
    public DateOnly? admission_date { get; set; }

    [Column(TypeName = "date")]
    public DateOnly? discharge_date { get; set; }

    [StringLength(200)]
    public string? reason { get; set; }

    [StringLength(13)]
    public string patient_umcn { get; set; } = null!;

    public int room_id { get; set; }

    [ForeignKey("patient_umcn")]
    [InverseProperty("admissions")]
    public virtual patient patient_umcnNavigation { get; set; } = null!;

    [ForeignKey("room_id")]
    [InverseProperty("admissions")]
    public virtual room room { get; set; } = null!;
}
