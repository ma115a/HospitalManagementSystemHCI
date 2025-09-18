using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("surgery")]
[Index("patient_umcn", Name = "fk_surgery_patient2_idx")]
[Index("surgeon_id", Name = "fk_surgery_surgeon1_idx")]
[Index("room_id", Name ="fk_surgery_room_idx")]
public partial class surgery
{
    [Key]
    public int surgery_id { get; set; }

    [StringLength(13)]
    public string? patient_umcn { get; set; }

    public int? surgeon_id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? date { get; set; } 
    
    [Column(TypeName = "datetime")]
    public DateTime? end_date { get; set; }
    
    
    public string? procedure { get; set; }

    public string? notes { get; set; }
    
    public int? duration { get; set; }
    
    public int? room_id { get; set; }
    
    
    public string? status { get; set; }
    
    [ForeignKey(nameof(room_id))]
    [InverseProperty(nameof(room.surgeries))]
    public virtual room? room { get; set; }
    
    [ForeignKey("patient_umcn")]
    [InverseProperty("surgeries")]
    public virtual patient? patient_umcnNavigation { get; set; }

    [ForeignKey("surgeon_id")]
    [InverseProperty("surgeries")]
    public virtual surgeon? surgeon { get; set; }
    
    
    public virtual ICollection<nurse> nurses { get; set; }
}
