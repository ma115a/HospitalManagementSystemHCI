using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    // public static IHost HostApp { get; } = 
    //     Host.CreateDefaultBuilder()
    //         .ConfigureAppConfiguration(cfg => 
    //             cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true))
    //         .ConfigureServices((ctx, services) =>
    //         {
    //             var cs = ctx.Configuration.GetConnectionString("Hms");
    //             
    //             services.AddDbContextFactory<HmsDbContext>()
    //         })
}