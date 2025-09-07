
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services;



public class AdmissionService
{
    private readonly IDbContextFactory<HmsDbContext> _factory;

    public AdmissionService(IDbContextFactory<HmsDbContext> factory) => _factory = factory;






    public async Task<admission> SaveAdmission(admission admission, room room, patient patient)
    {
        await using var context = await _factory.CreateDbContextAsync();
        admission.room_id = room.room_id;
        admission.patient_umcn = patient.umcn;
        context.admissions.Add(admission);
        await context.SaveChangesAsync();
        return admission;
    }

    public async Task<admission> UpdateAdmission(admission admission, room room, patient patient)
    {
        await using var context = await _factory.CreateDbContextAsync();
        var a = await context.admissions.FirstOrDefaultAsync(a => a.admission_id == admission.admission_id);
        if (a is null)
        {
            throw new Exception("Admission not found");
        }
        a.patient_umcn = patient.umcn;
        a.room_id = room.room_id;
        a.admission_date = admission.admission_date;
        a.discharge_date = admission.discharge_date;
        a.reason = admission.reason;
        await context.SaveChangesAsync();
        return admission;
        
    }


    public async Task<IEnumerable<admission>> GetAllAdmissionsForRoom(room room)
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.admissions.Where(a => a.room_id == room.room_id).Include(a => a.patient_umcnNavigation).ToListAsync();
    }


    public async Task<admission> DeleteAdmission(admission admission)
    {
        await using var context = await _factory.CreateDbContextAsync();
        context.admissions.Remove(admission);
        await context.SaveChangesAsync();
        return admission;
    }
}