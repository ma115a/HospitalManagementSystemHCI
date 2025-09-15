






using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Admin.Services;

public class DepartmentService
{
    private readonly IDbContextFactory<HmsDbContext> _factory;

    public DepartmentService(IDbContextFactory<HmsDbContext> factory) => _factory = factory;




    public async Task<department> CreateDepartment(department department, doctor headDoctor)
    {
       await using var context = await _factory.CreateDbContextAsync();
       department.doctor_employee_id = headDoctor.employee_id;
       context.departments.Add(department);
       await context.SaveChangesAsync();
       return department;

    }


    public async Task<room> CreateRoom(room room, department department)
    {
        await using var context = await _factory.CreateDbContextAsync();
        room.department_id = department.department_id;
        context.rooms.Add(room);
        await context.SaveChangesAsync();
        return room;

    }


    public async Task<IEnumerable<department>> GetAllDepartments()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.departments
            .Include(d => d.doctor_employee)     // <-- nav from department -> doctor
            .ThenInclude(doc => doc.employee)    // <-- nav from doctor -> employee
            .AsNoTracking()
            .ToListAsync();
    }

    public  IEnumerable<room> GetRoomsForDepartment(department department)
    {
         using var context =  _factory.CreateDbContext();
        return  context.rooms.Where(r => r.department_id == department.department_id).AsNoTracking().ToList();
    }


    public async Task<IEnumerable<room>> GetAllRooms()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.rooms.AsNoTracking().ToListAsync();
        
    }

    public async Task<IEnumerable<room>> GetSurgeryRooms()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.rooms.Include(r => r.department).Where(r => r.department.surgery_department == true && r.current_patients_number <= r.capacity).AsNoTracking().ToListAsync();
    }


    public async Task<department> DeleteDepartment(department department)
    {
       await using var context  = await _factory.CreateDbContextAsync();
       context.departments.Remove(department);
       await context.SaveChangesAsync();
       return department;
    }

    public async Task<room> DeleteRoom(room room)
    {
        await  using var context = await _factory.CreateDbContextAsync();
        context.rooms.Remove(room);
        await context.SaveChangesAsync();
        return room;
    }


    public async Task<IEnumerable<doctor>> GetAllDoctors()
    {
        await using var context = await _factory.CreateDbContextAsync();

        return await context.doctors.Include(d => d.employee).ToListAsync();
    }
}