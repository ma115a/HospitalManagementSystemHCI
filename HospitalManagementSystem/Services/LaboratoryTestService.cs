


using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services;


public class LaboratoryTestService
{

    private readonly IDbContextFactory<HmsDbContext> _factory;
    
    
    
    public LaboratoryTestService(IDbContextFactory<HmsDbContext> factory) =>  _factory = factory;


    public async Task<laboratory_test> SaveTest(laboratory_test test)
    {
        await using var context = await  _factory.CreateDbContextAsync();
        
        context.laboratory_tests.Add(test);
        await context.SaveChangesAsync();
        return test;
    }

}