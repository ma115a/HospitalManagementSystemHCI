using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace  HospitalManagementSystem.Admin.Services;

public class VehiclesService
{
    private readonly IDbContextFactory<HmsDbContext> _factory;
    
    public VehiclesService(IDbContextFactory<HmsDbContext> factory) => _factory = factory;


    public async Task<IEnumerable<vehicle>> GetAllVehicles()
    {

        await using var context = await _factory.CreateDbContextAsync();
        
        return await context.vehicles.ToListAsync();
    }

    public async Task<vehicle> UpdateVehicle(vehicle vehicle)
    {
        await using var context = await _factory.CreateDbContextAsync();
        var v = await context.vehicles.FirstOrDefaultAsync(x => x.vehicle_id == vehicle.vehicle_id);
        if (v == null)
        {
            throw new Exception("Vehicle not found");
        }
        
        v.notes =  vehicle.notes;
        v.brand = vehicle.brand;
        v.last_service = vehicle.last_service;
        v.registration = vehicle.registration;
        v.status = vehicle.status;
        await context.SaveChangesAsync();
        return v;
    }

    public async Task<vehicle> CreateVehicle(vehicle vehicle)
    {
        await using var context = await _factory.CreateDbContextAsync();
        
        context.vehicles.Add(vehicle);
        await context.SaveChangesAsync();
        return vehicle;
    }


    public async Task<vehicle> DeleteVehicle(vehicle vehicle)
    {
        await using var context = await _factory.CreateDbContextAsync();
        
        context.vehicles.Remove(vehicle);
        await context.SaveChangesAsync();
        return vehicle;
    }
    
}