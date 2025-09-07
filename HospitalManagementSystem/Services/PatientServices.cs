


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
        Console.WriteLine("get aall patient");
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
    
}