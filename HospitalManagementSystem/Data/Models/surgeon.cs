using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("surgeon")]
public partial class surgeon
{
    [Key]
    public int employee_id { get; set; }

    [ForeignKey("employee_id")]
    [InverseProperty("surgeon")]
    public virtual employee employee { get; set; } = null!;

    [InverseProperty("surgeon")]
    public virtual ICollection<surgery> surgeries { get; set; } = new List<surgery>();
}
