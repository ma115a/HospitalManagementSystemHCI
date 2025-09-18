using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("administrator")]
[Index("employee_id", Name = "fk_administrator_employee1_idx")]
public partial class administrator
{
    [Key]
    public int employee_id { get; set; }

    [InverseProperty("administrator_employee")]
    public virtual ICollection<admin_action> admin_actions { get; set; } = new List<admin_action>();

    [ForeignKey("employee_id")]
    [InverseProperty("administrator")]
    public virtual employee employee { get; set; } = null!;
}
