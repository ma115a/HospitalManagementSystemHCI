
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Admin.Services;

public class UserService
{



    private readonly IDbContextFactory<HmsDbContext> _factory;

    public UserService(IDbContextFactory<HmsDbContext> factory) => _factory = factory;

    public async Task<employee> UpdateUser(employee employee, string? newPassword, string? department)
    {
        await using var context = await _factory.CreateDbContextAsync();
        
        var e = await context.employees
            .Include(x => x.doctor)
            .Include(x => x.nurse)
            .Include(x => x.surgeon)
            .Include(x => x.laboratory_tehnician)
            .Include(x => x.administrator)
            .FirstOrDefaultAsync(x => x.employee_id == employee.employee_id);
        
        
        if(e is null)
            throw new Exception("Employee not found");
        e.name = employee.name;
        e.username = employee.username;
        e.email = employee.email;
        e.phone = employee.phone;
        e.notes = employee.notes;
        e.active = employee.active;


        if (!string.IsNullOrWhiteSpace(newPassword))
        {
           using var sha256 =  SHA256.Create(); 
           var hashed =  sha256.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
           e.password = Convert.ToBase64String(hashed);
        }
        
        
        await context.SaveChangesAsync();
        
        return e;
    }
    public async Task<employee> CreateUser(employee employee, string password, string role, string department)
    {
        await using var factory = await _factory.CreateDbContextAsync();
        
        var e = await factory.employees.FirstOrDefaultAsync(x => x.username == employee.username);
        if (e is not null) return null;
        
        employee.date_employed ??= DateOnly.FromDateTime(DateTime.Now);
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        string hashedpPassword = Convert.ToBase64String(hashedBytes);
        employee.password = hashedpPassword;
        factory.employees.Add(employee);
        await factory.SaveChangesAsync();
        
        await AddRoleSpecificRecords(factory, employee.employee_id, role, department);
        return employee;
    }


    public async Task<employee> GetUser(string username, string password)
    {
        await using var context = await _factory.CreateDbContextAsync();
        var employee = await context.employees.Include(e => e.nurse).Include(e => e.doctor).Include(e => e.surgeon).Include(e => e.laboratory_tehnician).Include(e => e.administrator).FirstOrDefaultAsync(e => e.username == username);
        if (employee is null) return null;
        
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var hashedPasswrd =  Convert.ToBase64String(hashedBytes);
        if(employee.password == hashedPasswrd) return employee;
        throw new Exception("Password not match");
    }

    public async Task<employee> DeleteUser(employee employee)
    {
        await using var context = await _factory.CreateDbContextAsync();
        
        var e = await context.employees
        .Include(x => x.doctor)
        .Include(x => x.nurse)
        .Include(x => x.surgeon)
        .Include(x => x.laboratory_tehnician)
        .Include(x => x.administrator)
        .FirstOrDefaultAsync(x => x.employee_id == employee.employee_id);


        if (e is null)
        {
            throw new Exception("Employee not found");
        }

        e.active = false;
        await context.SaveChangesAsync();
        return e;
    }


    private async Task AddRoleSpecificRecords(HmsDbContext context, int eId, string role,  string department)
    {
        switch (role.ToLower())
        {
           case "doctor":
               context.doctors.Add(
                   new doctor{
                       employee_id = eId,
                       specialty = department});
               break;
           case "administrator":
               context.administrators.Add(new administrator
                   {
                       employee_id = eId
                   });
               break;
           case "nurse":
               context.nurses.Add(
                   new nurse
                   {
                       employee_id = eId,
                       specialty = department

                   });
               break;
           case "surgeon":
               context.surgeons.Add(
                   new surgeon
                   {
                       employee_id = eId,
                   });
               break;
           case "lab technician":
               context.laboratory_tehnicians.Add(
                   new laboratory_tehnician
                   {
                       employee_id = eId,
                   });
               break;
        }

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<employee>> GetAllEmployees()
    {
        Console.WriteLine("called all empl");
        using var  factory = await _factory.CreateDbContextAsync();
        return await factory.employees
            .Include(e => e.doctor)
            .Include(e => e.nurse)
            .Include(e => e.surgeon)
            .Include(e => e.laboratory_tehnician)
            .Include(e => e.administrator)
            .ToListAsync();
    }


    public async Task<IEnumerable<laboratory_tehnician>> GetLabWorkers()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.laboratory_tehnicians.Include(t => t.employee).ToListAsync();
    }

    public async Task<IEnumerable<doctor>> GetDoctors()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.doctors.Include(d => d.employee).ToListAsync();
    }

    public async Task<IEnumerable<nurse>> GetNurseWorkers()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.nurses.Include(n => n.employee).AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<surgeon>> GetSurgeons()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.surgeons.Include(s => s.employee).ToListAsync();
    }
    



}
