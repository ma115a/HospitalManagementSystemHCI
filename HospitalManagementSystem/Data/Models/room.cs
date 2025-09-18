using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("room")]
[Index("department_id", Name = "fk_room_department2_idx")]
public partial class room
{
    [Key]
    public int room_id { get; set; }

    public int department_id { get; set; }

    public sbyte? capacity { get; set; }
    
    public int? number { get; set; }

    public int? current_patients_number { get; set; }

    [InverseProperty("room")]
    public virtual ICollection<admission> admissions { get; set; } = new List<admission>();
    
    [InverseProperty(nameof(surgery.room))]
    public virtual ICollection<surgery> surgeries { get; set; } = new List<surgery>();
    

    [ForeignKey("department_id")]
    [InverseProperty("rooms")]
    public virtual department department { get; set; } = null!;
}
