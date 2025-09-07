

using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Nurse.Services;


public class DoctorService
{

    private readonly IDbContextFactory<HmsDbContext> _factory;
    
    
    public  DoctorService(IDbContextFactory<HmsDbContext> factory) => _factory = factory;




    public async Task<IEnumerable<doctor>> GetAllDoctors()
    {
        await using var context = await _factory.CreateDbContextAsync();
        
       return await context.doctors.Include(d => d.employee).OrderBy(d => d.employee.name).ToListAsync();
        
    }
}