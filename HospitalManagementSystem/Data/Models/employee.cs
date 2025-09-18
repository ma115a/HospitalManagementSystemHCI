using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("employee")]
public partial class employee
{
    [Key]
    public int employee_id { get; set; }

    [StringLength(50)]
    public string name { get; set; } = null!;

    [StringLength(20)]
    public string username { get; set; } = null!;

    [StringLength(25)]
    public string email { get; set; } = null!;

    [StringLength(300)]
    public string password { get; set; } = null!;

    [StringLength(20)]
    public string phone { get; set; } = null!;

    public DateOnly? date_employed { get; set; }

    public bool? active { get; set; }

    public string? notes { get; set; }

    [InverseProperty("employee")]
    public virtual administrator? administrator { get; set; }

    [InverseProperty("employee")]
    public virtual doctor? doctor { get; set; }

    [InverseProperty("employee")]
    public virtual laboratory_tehnician? laboratory_tehnician { get; set; }

    [InverseProperty("employee")]
    public virtual nurse? nurse { get; set; }

    [InverseProperty("employee")]
    public virtual surgeon? surgeon { get; set; }
}
