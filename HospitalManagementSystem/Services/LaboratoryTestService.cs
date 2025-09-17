


using System.IO.Pipes;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services;


public class LaboratoryTestService
{

    private readonly IDbContextFactory<HmsDbContext> _factory;
    
    
    
    public LaboratoryTestService(IDbContextFactory<HmsDbContext> factory) =>  _factory = factory;


    public async Task<laboratory_test> SaveTest(laboratory_test test, patient patient, laboratory_tehnician tehnician)
    {
        
        await using var context = await  _factory.CreateDbContextAsync();
        test.patient_umcn = patient.umcn;
        test.laboratory_tehnician_id = tehnician.employee_id;
        test.nurse_id = 2; 
        
        test.date = DateOnly.FromDateTime(DateTime.Now);
        
        context.laboratory_tests.Add(test);
        await context.SaveChangesAsync();
        return test;
    }


    public async Task<laboratory_test> DeleteTest(laboratory_test test)
    {
        await using var context = await  _factory.CreateDbContextAsync();
        var t = await context.laboratory_tests.FirstOrDefaultAsync(x => x.laboratory_test_id == test.laboratory_test_id);
        if (t is null) throw new Exception("Test not found");
        context.laboratory_tests.Remove(t);
        await  context.SaveChangesAsync();
        return t;
    }


    public async Task<laboratory_test> EditTest(laboratory_test test, patient patient, laboratory_tehnician tehnician)
    {
        await using var context = await _factory.CreateDbContextAsync();
        var t = await context.laboratory_tests.FirstOrDefaultAsync(x => x.laboratory_test_id == test.laboratory_test_id);
        if (t is null) throw new Exception("Test not found");

        t.patient_umcn = patient.umcn;
        t.laboratory_tehnician_id = tehnician.employee_id;
        t.name = test.name;
        t.description = test.description;
        
        await context.SaveChangesAsync();
        return t;



    }


    public async Task<IEnumerable<laboratory_test>> GetAllTests()
    {
        await using var context = await  _factory.CreateDbContextAsync();
        return await context.laboratory_tests.Include(l => l.patient_umcnNavigation).Include(l => l.doctor).ThenInclude(doc => doc.employee).Include(l => l.nurse).ThenInclude(nurse => nurse.employee).AsNoTracking().ToListAsync();
    }


    public async Task<IEnumerable<laboratory_test_result>> GetAllTestResults()
    {
        await using var context = await  _factory.CreateDbContextAsync();
        return await context.laboratory_test_results.Include(r => r.laboratory_test).AsNoTracking().ToListAsync();

    }


    public async Task<laboratory_test> StartTest(laboratory_test test)
    {
        await using var context = await  _factory.CreateDbContextAsync();
        var t = await context.laboratory_tests.FirstOrDefaultAsync(x => x.laboratory_test_id == test.laboratory_test_id);
        if (t is null) throw new Exception("Test not found");
        t.status = "In Progress";
        await context.SaveChangesAsync();
        return t;
    }


    public async Task<IEnumerable<laboratory_test>> GetScheduledTests()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.laboratory_tests.Where(t => t.status == "Scheduled").Include(t => t.patient_umcnNavigation).AsNoTracking().ToListAsync();
    }


    public async Task<laboratory_test_result> SaveTestResult(laboratory_test test, laboratory_test_result result) 
    {
        await using var context = await  _factory.CreateDbContextAsync();
        var t = await context.laboratory_tests.FirstOrDefaultAsync(t => t.laboratory_test_id == test.laboratory_test_id);
        if (t is null) throw new Exception("Test not found");
        t.status = test.status;
        await context.SaveChangesAsync();
        
        result.laboratory_test_id = test.laboratory_test_id;
        context.laboratory_test_results.Add(result);
        await context.SaveChangesAsync();
        return result;

    }

    public async Task<IEnumerable<laboratory_test>> GetFinishedLaboratoryTests()
    {
        await using var context = await  _factory.CreateDbContextAsync();
        return await context.laboratory_tests.Where(t => t.status != "Scheduled").Include(t => t.laboratory_test_result).Include(t => t.patient_umcnNavigation).Include(t => t.doctor).ThenInclude(doc => doc.employee).Include(t => t.nurse).ThenInclude(nu => nu.employee).AsNoTracking().ToListAsync();
    }


    public async Task<IEnumerable<laboratory_test>> GetAllTestsForPatient(patient patient)
    {
        await using var context = await  _factory.CreateDbContextAsync();
        return await context.laboratory_tests.Where(l => l.patient_umcn == patient.umcn).Include(t => t.doctor).ThenInclude(doc => doc.employee).Include(t => t.nurse).ThenInclude(nu => nu.employee).AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<laboratory_test_result>> GetAllTestResultsForPatient(patient patient)
    {
        await using var context = await  _factory.CreateDbContextAsync();
        var patientTests = await context.laboratory_tests.Where(t => t.patient_umcn == patient.umcn)
            .Select(t => t.laboratory_test_id).ToListAsync();
        
        return await context.laboratory_test_results.Where(r =>  patientTests.Contains(r.laboratory_test_id)).Include(t => t.laboratory_test).AsNoTracking().ToListAsync();
    }
}