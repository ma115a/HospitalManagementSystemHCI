using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("laboratory_test_result")]
public partial class laboratory_test_result
{
    [Key]
    public int laboratory_test_id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? date { get; set; }

    [StringLength(200)]
    public string? result_data { get; set; }

    [ForeignKey("laboratory_test_id")]
    [InverseProperty("laboratory_test_result")]
    public virtual laboratory_test laboratory_test { get; set; } = null!;
}
