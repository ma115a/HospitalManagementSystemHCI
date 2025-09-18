using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("laboratory_tehnician")]
public partial class laboratory_tehnician
{
    [Key]
    public int employee_id { get; set; }

    [ForeignKey("employee_id")]
    [InverseProperty("laboratory_tehnician")]
    public virtual employee employee { get; set; } = null!;

    [InverseProperty("laboratory_tehnician")]
    public virtual ICollection<laboratory_test> laboratory_tests { get; set; } = new List<laboratory_test>();
}
