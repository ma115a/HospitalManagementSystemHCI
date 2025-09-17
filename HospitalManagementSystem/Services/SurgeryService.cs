


using System.Collections.ObjectModel;
using System.Runtime;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Surgeon.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services;



public class SurgeryService
{
    private readonly IDbContextFactory<HmsDbContext> _factory;


    public SurgeryService(IDbContextFactory<HmsDbContext> factory)
    {
        _factory = factory;
    }



    public async Task<surgery> SaveSurgery(surgery surgery, patient patient, room room, ObservableCollection<NurseItem> nurses)
    {
        await using var context = await _factory.CreateDbContextAsync();
        surgery.patient_umcn = patient.umcn;
        surgery.room_id = room.room_id;
        surgery.surgeon_id = 3;
        surgery.status = "Scheduled";
        context.surgeries.Add(surgery);
        await context.SaveChangesAsync();

        foreach (var item in nurses)
        {
            context.surgery_has_nurses.Add(new surgery_has_nurse
            {
                nurse_nurse_id = item.Id,
                surgery_surgery_id = surgery.surgery_id
            });
        }
        await context.SaveChangesAsync();
        return surgery;

    }


    public async Task<surgery> UpdateSurgery(surgery surgery)
    {
        await using var context = await _factory.CreateDbContextAsync();
        var s =  context.surgeries.FirstOrDefault(s => s.surgery_id == surgery.surgery_id);
        if (s is null) throw new Exception("Surgery not found");
        s.date = surgery.date;
        s.duration = surgery.duration;
        s.end_date = surgery.end_date;
        s.notes = surgery.notes;
        s.status = surgery.status;
        s.patient_umcn = surgery.patient_umcn;
        await context.SaveChangesAsync();
        return s;
    }


    public async Task<surgery> DeleteSurgery(surgery surgery)
    {
        await using var context = await _factory.CreateDbContextAsync();
        var s = context.surgeries.FirstOrDefault(s => s.surgery_id == surgery.surgery_id);
        if (s is null) throw new Exception("Surgery not found");
        context.surgeries.Remove(s);
        await context.SaveChangesAsync();
        return s;
    }


    public async Task<IEnumerable<surgery>> GetAllSurgeries()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.surgeries.AsNoTracking().AsSplitQuery().Include(s => s.patient_umcnNavigation).Include(s => s.surgeon)
            .ThenInclude(su => su.employee).Include(s => s.nurses).ThenInclude(n => n.employee).Include(s => s.room).ToListAsync();
    }

    private bool IntervalsOverlap(DateTime aStart, DateTime aEnd, DateTime bStart, DateTime bEnd)
    {
        return aStart < bEnd && bStart < aEnd;
    }
    
    
    public async Task<IEnumerable<nurse>> GetAvailableNurses(surgery surgery)
    {
        await using var context = await _factory.CreateDbContextAsync();

        DateTime? targetStart = surgery.date;
        var time = TimeSpan.FromMinutes((double)surgery.duration);
        DateTime? targetEnd = targetStart.Value + time;
        
        
        var overlappingSurgeryIds = await context.surgeries
            .Where(s => s.surgery_id != surgery.surgery_id) // Exclude the target surgery if editing
            .Where(s => s.date < targetEnd && s.end_date > targetStart) // Simple overlap check using end_date
            .Select(s => s.surgery_id)
            .ToListAsync();

        var busyNurseIds = await context.surgery_has_nurses
            .Where(shn => overlappingSurgeryIds.Contains(shn.surgery_surgery_id))
            .Select(shn => shn.nurse_nurse_id)
            .Distinct()
            .ToListAsync();

        var alreadyAssignedIds = await context.surgery_has_nurses
            .Where(shn => shn.surgery_surgery_id == surgery.surgery_id)
            .Select(shn => shn.nurse_nurse_id)
            .ToListAsync();
        
        
        var unavailableIds = busyNurseIds.Union(alreadyAssignedIds).ToList();


        var availableNurses = await context.nurses.Where(n => !unavailableIds.Contains(n.employee_id)).Include(n => n.employee).ToListAsync();
        
        return availableNurses;
    }


    public async Task<IEnumerable<nurse>> GetAvailableNursesForSurgery(DateTime? startDate, int? duration)
    {
        await using var context = await _factory.CreateDbContextAsync();
        DateTime? endDate = startDate.Value + TimeSpan.FromMinutes((double)duration);
        Console.WriteLine(endDate);
        
        var overlappingSurgeryIds = await context.surgeries
            .Where(s => s.date < endDate && s.end_date > startDate) // Simple overlap check using end_date
            .Select(s => s.surgery_id)
            .ToListAsync();
        
        var busyNurseIds = await context.surgery_has_nurses
            .Where(shn => overlappingSurgeryIds.Contains(shn.surgery_surgery_id))
            .Select(shn => shn.nurse_nurse_id)
            .Distinct()
            .ToListAsync();
        
        
        var availableNurses = await context.nurses.Where(n => !busyNurseIds.Contains(n.employee_id)).Include(n => n.employee).ToListAsync();
        
        return availableNurses;
    }


    public async Task<nurse> AssignNurseToSurgery(surgery surgery, nurse nurse)
    {
        
        await using var context = await _factory.CreateDbContextAsync();
        
        context.surgery_has_nurses.Add(new surgery_has_nurse
        {
            nurse_nurse_id = nurse.employee_id,
            surgery_surgery_id = surgery.surgery_id
        });
        
        await context.SaveChangesAsync();
        return nurse;
    }


    public async Task<nurse> RemoveNurseFromSurgery(surgery surgery, nurse nurse)
    {
        await using var context = await _factory.CreateDbContextAsync();
        var n = context.surgery_has_nurses.FirstOrDefault(s => s.surgery_surgery_id == surgery.surgery_id &&  s.nurse_nurse_id == nurse.employee_id);
        if(n is null) throw new Exception("Surgery or nurse not found");
        context.surgery_has_nurses.Remove(n);
        await context.SaveChangesAsync();
        return nurse;
    }


    public async Task<IEnumerable<surgery>> GetSurgeriesForPatient(patient patient)
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.surgeries.Where(s => s.patient_umcn == patient.umcn).Include(s => s.room).Include(s => s.surgeon).ThenInclude(su => su.employee).AsNoTracking().ToListAsync();
    }
}

