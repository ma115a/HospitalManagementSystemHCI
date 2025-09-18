

using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Nurse.Services;



public class AppointmentService
{

    private readonly IDbContextFactory<HmsDbContext> _factory;
    
    public AppointmentService(IDbContextFactory<HmsDbContext> factory) => _factory = factory;



    public async Task<appointment> SaveAppointment(appointment appointment, doctor doctor, patient patient)
    {
        await using var context = await _factory.CreateDbContextAsync();


        appointment.doctor_id = doctor.employee_id;
        appointment.patient_umcn = patient.umcn;
        
        
        context.appointments.Add(appointment);
        await context.SaveChangesAsync();
        return appointment;
    }


    public async Task<IEnumerable<appointment>> GetAllAppointments()
    {
       await using var context =  await _factory.CreateDbContextAsync(); 
       return await context.appointments.Include(a => a.patient_umcnNavigation).Include(a => a.doctor).ThenInclude(d=> d.employee).OrderBy(a => a.date).ToListAsync();
    }


    public async Task<appointment> DeleteAppointment(appointment appointment)
    {
        await using var context = await _factory.CreateDbContextAsync();
        context.appointments.Remove(appointment);
        await context.SaveChangesAsync();
        return appointment;
    }


    public async Task<IEnumerable<appointment>> GetAppointmentsForDoctor(doctor doctor)
    {
       await  using var context = await _factory.CreateDbContextAsync(); 
       return await  context.appointments.Where(a => a.doctor_id == doctor.employee_id).ToListAsync();
    }


    public async Task<IEnumerable<appointment>> GetAppointmentsForStatus(string status)
    {
        await  using var context = await _factory.CreateDbContextAsync();
        return await context.appointments.Where(a => a.status == status).ToListAsync();
    }

    public async Task<appointment> UpdateAppointment(appointment appointment)
    {
        await using var context = await _factory.CreateDbContextAsync();
        var a = await context.appointments.FirstOrDefaultAsync(a => a.appointment_id == appointment.appointment_id);
        if(a is null) throw new Exception("Appointment not found");
        
        a.date = appointment.date;
        a.patient_umcn = appointment.patient_umcn;
        a.doctor_id = appointment.doctor_id;
        a.notes =  appointment.notes;
        a.status = appointment.status;
        await context.SaveChangesAsync();
        return a;
    }

}