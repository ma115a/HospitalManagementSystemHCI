using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data.Models;

[Table("laboratory_test")]
[Index("doctor_id", Name = "fk_laboratory_test_doctor2_idx")]
[Index("nurse_id", Name ="fk_laboratory_test_nurse_idx")]
[Index("laboratory_tehnician_id", Name = "fk_laboratory_test_laboratory_tehnician1_idx")]
[Index("patient_umcn", Name = "fk_laboratory_test_patient2_idx")]
public partial class laboratory_test
{
    [Key]
    public int laboratory_test_id { get; set; }

    public int? laboratory_tehnician_id { get; set; }

    public int? doctor_id { get; set; }
    
    public int? nurse_id { get; set; }

    [StringLength(13)]
    public string? patient_umcn { get; set; }

    [StringLength(100)]
    public string? description { get; set; }
    
    [StringLength(100)]
    public string? name { get; set; }
    
    public DateOnly? date { get; set; }
    
    [StringLength(45)]
    public string? status { get; set; }

    [ForeignKey("doctor_id")]
    [InverseProperty("laboratory_tests")]
    public virtual doctor? doctor { get; set; }
    
    [ForeignKey("nurse_id")]
    [InverseProperty("laboratory_tests")]
    public virtual nurse? nurse { get; set; }
    

    [ForeignKey("laboratory_tehnician_id")]
    [InverseProperty("laboratory_tests")]
    public virtual laboratory_tehnician? laboratory_tehnician { get; set; }

    [InverseProperty("laboratory_test")]
    public virtual laboratory_test_result? laboratory_test_result { get; set; }
    

    [ForeignKey("patient_umcn")]
    [InverseProperty("laboratory_tests")]
    public virtual patient? patient_umcnNavigation { get; set; }
}
