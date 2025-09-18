


using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

public class PatientService
{
    
    private readonly IDbContextFactory<HmsDbContext> _factory;
    
    public PatientService(IDbContextFactory<HmsDbContext> factory) => _factory = factory;





    public async Task<patient> CreatePatient(patient patient)
    {
        await using var context = await _factory.CreateDbContextAsync();
        context.patients.Add(patient);
        await context.SaveChangesAsync();
        return patient;
    }


    public async Task<IEnumerable<patient>> GetAllPatients()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.patients.OrderBy(p => p.name).ToListAsync();
    }


    public async Task<patient> UpdatePatient(patient patient)
    {
        await using var context = await _factory.CreateDbContextAsync();

        var p = await context.patients.FirstOrDefaultAsync(x => x.umcn == patient.umcn);
        if(p is null)  throw new Exception("Patient not found");
        p.umcn = patient.umcn;
        p.name = patient.name;
        p.phone = patient.phone;
        p.notes = patient.notes;
        await context.SaveChangesAsync();
        return p;
    }


    public async Task<patient> DeletePatient(patient patient)
    {
        await using var context = await _factory.CreateDbContextAsync();
        context.patients.Remove(patient);
        await context.SaveChangesAsync();
        return patient;
    }


    public async Task<medical_record> SaveMedicalRecord(medical_record record, int id)
    {
        await using var context = await _factory.CreateDbContextAsync();
        record.doctor_id = id;
        context.medical_records.Add(record);
        await context.SaveChangesAsync();
        return record;
    }

    public async Task<medical_record> UpdateMedicalRecord(medical_record record)
    {
        await using var context = await _factory.CreateDbContextAsync();
        var m = await context.medical_records.FirstOrDefaultAsync(med => med.medical_record_id == record.medical_record_id);
        if(m is null) throw new Exception("Medical record not found");
        m.date = record.date;
        m.notes = record.notes;
        m.diagnosis = record.diagnosis;
        await context.SaveChangesAsync();
        return m;

    }

    public async Task<medical_record> DeleteMedicalRecord(medical_record record)
    {
        await using var context = await _factory.CreateDbContextAsync();
        context.medical_records.Remove(record);
        await context.SaveChangesAsync();
        return record;
    }

    public async Task<IEnumerable<medical_record>> GetMedicalRecords(patient patient)
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.medical_records.Where(m => m.patient_umcn == patient.umcn).Include(m => m.doctor).ThenInclude(doc => doc.employee).AsNoTracking().ToListAsync();
    }


    public async Task<prescription> SavePrescription(prescription prescription)
    {
        await using var context = await _factory.CreateDbContextAsync();
        
        context.prescriptions.Add(prescription);
        await context.SaveChangesAsync();
        return prescription;

    }


    public async Task<prescription> UpdatePrescription(prescription prescription)
    {
        await using var context = await _factory.CreateDbContextAsync();
        var p = await context.prescriptions.FirstOrDefaultAsync(p=> p.prescription_id == prescription.prescription_id);
        if(p is null) throw new Exception("Prescription not found");
        p.date = prescription.date;
        p.medication = prescription.medication;
        p.dosage = prescription.dosage;
        p.duration = prescription.duration;
        p.notes = prescription.notes;
        await context.SaveChangesAsync();
        return p;
    }


    public async Task<IEnumerable<prescription>> GetPrescriptions(patient patient)
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.prescriptions.Where(p => p.patient_umcn == patient.umcn).AsNoTracking().ToListAsync();
    }
    
}