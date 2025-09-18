using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("nurse")]
public partial class nurse
{
    [Key]
    public int employee_id { get; set; }

    [StringLength(45)]
    public string? specialty { get; set; }

    [ForeignKey("employee_id")]
    [InverseProperty("nurse")]
    public virtual employee employee { get; set; } = null!;
    
    
    [InverseProperty("nurse")]
    public virtual ICollection<laboratory_test> laboratory_tests { get; set; } = new List<laboratory_test>();
    
    
    public virtual ICollection<surgery> surgeries { get; set; }
}
