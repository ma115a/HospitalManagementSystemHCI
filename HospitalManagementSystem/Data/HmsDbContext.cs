using System;
using System.Collections.Generic;
using HospitalManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data;

public partial class HmsDbContext : DbContext
{
    public HmsDbContext(DbContextOptions<HmsDbContext> options)
        : base(options)
    {
    }

    // public virtual DbSet<admin_action> admin_actions { get; set; }

    public virtual DbSet<administrator> administrators { get; set; }

    public virtual DbSet<admission> admissions { get; set; }

    public virtual DbSet<appointment> appointments { get; set; }

    public virtual DbSet<department> departments { get; set; }

    public virtual DbSet<doctor> doctors { get; set; }

    public virtual DbSet<employee> employees { get; set; }

    public virtual DbSet<institution> institutions { get; set; }

    // public virtual DbSet<inventory> inventories { get; set; }

    public virtual DbSet<laboratory_tehnician> laboratory_tehnicians { get; set; }

    public virtual DbSet<laboratory_test> laboratory_tests { get; set; }

    public virtual DbSet<laboratory_test_result> laboratory_test_results { get; set; }

    public virtual DbSet<medical_record> medical_records { get; set; }

    // public virtual DbSet<medication> medications { get; set; }

    // public virtual DbSet<medication_has_inventory> medication_has_inventories { get; set; }

    public virtual DbSet<nurse> nurses { get; set; }

    public virtual DbSet<patient> patients { get; set; }

    public virtual DbSet<prescription> prescriptions { get; set; }

    public virtual DbSet<room> rooms { get; set; }

    public virtual DbSet<surgeon> surgeons { get; set; }

    public virtual DbSet<surgery> surgeries { get; set; }

    public virtual DbSet<surgery_has_nurse> surgery_has_nurses { get; set; }

    public virtual DbSet<vehicle> vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        /*modelBuilder.Entity<admin_action>(entity =>
        {
            entity.HasKey(e => new { e.action_id, e.administrator_employee_id })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.action_id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.administrator_employee).WithMany(p => p.admin_actions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_admin_action_administrator1");
        });*/

        modelBuilder.Entity<administrator>(entity =>
        {
            entity.HasKey(e => e.employee_id).HasName("PRIMARY");

            entity.Property(e => e.employee_id).ValueGeneratedNever();

            entity.HasOne(d => d.employee).WithOne(p => p.administrator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_administrator_employee1");
        });

        modelBuilder.Entity<admission>(entity =>
        {
            entity.HasKey(e => e.admission_id).HasName("PRIMARY");

            entity.HasOne(d => d.patient_umcnNavigation).WithMany(p => p.admissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_admission_patient2");

            entity.HasOne(d => d.room).WithMany(p => p.admissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_admission_room2");
        });

        modelBuilder.Entity<appointment>(entity =>
        {
            entity.HasKey(e => e.appointment_id).HasName("PRIMARY");

            entity.HasOne(d => d.doctor).WithMany(p => p.appointments).HasConstraintName("fk_appointment_doctor2");

            entity.HasOne(d => d.patient_umcnNavigation).WithMany(p => p.appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_appointment_patient2");
        });

        modelBuilder.Entity<department>(entity =>
        {
            entity.HasKey(e => e.department_id).HasName("PRIMARY");

            entity.HasOne(d => d.doctor_employee).WithMany(p => p.departments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_department_doctor1");
        });

        modelBuilder.Entity<doctor>(entity =>
        {
            entity.HasKey(e => e.employee_id).HasName("PRIMARY");

            entity.Property(e => e.employee_id).ValueGeneratedNever();

            entity.HasOne(d => d.employee).WithOne(p => p.doctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_doctor_employee1");
        });

        modelBuilder.Entity<employee>(entity =>
        {
            entity.HasKey(e => e.employee_id).HasName("PRIMARY");
        });

        modelBuilder.Entity<institution>(entity =>
        {
            entity.HasKey(e => e.institution_id).HasName("PRIMARY");
        });

        /*modelBuilder.Entity<inventory>(entity =>
        {
            entity.HasKey(e => e.inventory_id).HasName("PRIMARY");
        });*/

        modelBuilder.Entity<laboratory_tehnician>(entity =>
        {
            entity.HasKey(e => e.employee_id).HasName("PRIMARY");

            entity.Property(e => e.employee_id).ValueGeneratedNever();

            entity.HasOne(d => d.employee).WithOne(p => p.laboratory_tehnician)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_laboratory_tehnician_employee1");
        });

        modelBuilder.Entity<laboratory_test>(entity =>
        {
            entity.HasKey(e => e.laboratory_test_id).HasName("PRIMARY");

            entity.HasOne(d => d.doctor).WithMany(p => p.laboratory_tests).HasConstraintName("fk_laboratory_test_doctor2");
            
            entity.HasOne(d => d.nurse).WithMany(p => p.laboratory_tests).HasConstraintName("fk_laboratory_test_nurse");

            entity.HasOne(d => d.laboratory_tehnician).WithMany(p => p.laboratory_tests).HasConstraintName("fk_laboratory_test_laboratory_tehnician1");

            entity.HasOne(d => d.patient_umcnNavigation).WithMany(p => p.laboratory_tests).HasConstraintName("fk_laboratory_test_patient2");
        });

        modelBuilder.Entity<laboratory_test_result>(entity =>
        {
            entity.HasKey(e => e.laboratory_test_id).HasName("PRIMARY");

            entity.Property(e => e.laboratory_test_id).ValueGeneratedNever();

            entity.HasOne(d => d.laboratory_test).WithOne(p => p.laboratory_test_result)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_laboratory_test_result_laboratory_test1");
        });

        modelBuilder.Entity<medical_record>(entity =>
        {
            entity.HasKey(e => e.medical_record_id).HasName("PRIMARY");

            entity.HasOne(d => d.doctor).WithMany(p => p.medical_records).HasConstraintName("fk_medical_record_doctor1");

            entity.HasOne(d => d.patient_umcnNavigation).WithMany(p => p.medical_records)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_medical_record_patient1");
        });

        /*modelBuilder.Entity<medication>(entity =>
        {
            entity.HasKey(e => e.medication_id).HasName("PRIMARY");
        });

        modelBuilder.Entity<medication_has_inventory>(entity =>
        {
            entity.HasOne(d => d.inventory_inventory).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_medication_has_inventory_inventory1");

            entity.HasOne(d => d.medication_medication).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_medication_has_inventory_medication1");
        });*/

        modelBuilder.Entity<nurse>(entity =>
        {
            entity.HasKey(e => e.employee_id).HasName("PRIMARY");

            entity.Property(e => e.employee_id).ValueGeneratedNever();

            entity.HasOne(d => d.employee).WithOne(p => p.nurse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_nurse_employee1");
        });

        modelBuilder.Entity<patient>(entity =>
        {
            entity.HasKey(e => e.umcn).HasName("PRIMARY");
        });

        modelBuilder.Entity<prescription>(entity =>
        {
            entity.HasKey(e => e.prescription_id).HasName("PRIMARY");

            entity.HasOne(d => d.doctor).WithMany(p => p.prescriptions).HasConstraintName("fk_prescription_doctor2");

            // entity.HasOne(d => d.medication).WithMany(p => p.prescriptions).HasConstraintName("fk_prescription_medication1");

            entity.HasOne(d => d.patient_umcnNavigation).WithMany(p => p.prescriptions).HasConstraintName("fk_prescription_patient2");
        });

        modelBuilder.Entity<room>(entity =>
        {
            entity.HasKey(e => e.room_id).HasName("PRIMARY");

            entity.HasOne(d => d.department).WithMany(p => p.rooms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_room_department2");
        });

        modelBuilder.Entity<surgeon>(entity =>
        {
            entity.HasKey(e => e.employee_id).HasName("PRIMARY");

            entity.Property(e => e.employee_id).ValueGeneratedNever();

            entity.HasOne(d => d.employee).WithOne(p => p.surgeon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_surgeon_employee1");
        });

        // modelBuilder.Entity<surgery>(entity =>
        // {
        //     entity.HasKey(e => e.surgery_id).HasName("PRIMARY");
        //
        //     entity.HasOne(d => d.patient_umcnNavigation).WithMany(p => p.surgeries).HasConstraintName("fk_surgery_patient2");
        //     
        //     
        //     entity.HasOne(d => d.room).WithMany(p => p.surgeries).HasConstraintName("fk_surgery_room");
        //
        //     entity.HasOne(d => d.surgeon).WithMany(p => p.surgeries).HasConstraintName("fk_surgery_surgeon1");
        // });
        modelBuilder.Entity<surgery>(entity =>
        {
            entity.HasKey(e => e.surgery_id).HasName("PRIMARY");

            entity.HasOne(d => d.patient_umcnNavigation)
                .WithMany(p => p.surgeries)
                .HasConstraintName("fk_surgery_patient2");

            entity.HasOne(d => d.room)
                .WithMany(p => p.surgeries)
                .HasConstraintName("fk_surgery_room");

            entity.HasOne(d => d.surgeon)
                .WithMany(p => p.surgeries)
                .HasConstraintName("fk_surgery_surgeon1");

            // 🔻 MANY-TO-MANY: surgery ↔ nurse preko postojeće tabele surgery_has_nurse
            entity.HasMany(s => s.nurses)
                .WithMany(n => n.surgeries)
                .UsingEntity<surgery_has_nurse>(
                    j => j.HasOne(x => x.nurse_nurse)
                        .WithMany()
                        .HasForeignKey(x => x.nurse_nurse_id)
                        .OnDelete(DeleteBehavior.Restrict)           // NO ACTION u DB
                        .HasConstraintName("fk_surgery_has_nurse_nurse1"),
                    j => j.HasOne(x => x.surgery_surgery)
                        .WithMany()
                        .HasForeignKey(x => x.surgery_surgery_id)
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_surgery_has_nurse_surgery1"),
                    j =>
                    {
                        j.ToTable("surgery_has_nurse");
                        j.HasKey(x => new { x.surgery_surgery_id, x.nurse_nurse_id });
                    });
        });

        modelBuilder.Entity<surgery_has_nurse>(entity =>
        {
            entity.ToTable("surgery_has_nurse");

            entity.HasKey(e => new { e.surgery_surgery_id, e.nurse_nurse_id });


            entity.HasOne(e => e.surgery_surgery)
                .WithMany() // or .WithMany(x => x.surgery_has_nurses) if you have a collection nav
                .HasForeignKey(e => e.surgery_surgery_id)
                .OnDelete(DeleteBehavior.Restrict)   // matches NO ACTION in DB
                .HasConstraintName("fk_surgery_has_nurse_surgery1");

            entity.HasOne(e => e.nurse_nurse)
                .WithMany()
                .HasForeignKey(e => e.nurse_nurse_id)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_surgery_has_nurse_nurse1");
        });

        modelBuilder.Entity<vehicle>(entity =>
        {
            entity.HasKey(e => e.vehicle_id).HasName("PRIMARY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
