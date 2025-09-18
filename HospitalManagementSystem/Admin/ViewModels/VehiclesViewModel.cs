
using System.Collections.ObjectModel;
using HospitalManagementSystem.Data.Models;





using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalManagementSystem.Admin.Services;

public partial class VehiclesViewModel : ObservableObject
{
    
    
    private readonly VehiclesService _vehiclesService;
    [ObservableProperty] private vehicle? selectedVehicle;
    
    [ObservableProperty]
    private ObservableCollection<vehicle> vehicles = new();


    private bool isEditing = false;


    [ObservableProperty] private bool _isControlsEnabled = false;
    
    
    [ObservableProperty]
    private bool _isControls2Enabled = false;


    public VehiclesViewModel(VehiclesService service)
    {
       _vehiclesService = service;
       _ = LoadData();
    }


    public DateTime? LastServiceDateTime
    {
        get => SelectedVehicle?.last_service?.ToDateTime(TimeOnly.MinValue);
        set
        {
            if (SelectedVehicle != null)
            {
                SelectedVehicle.last_service = value.HasValue ? DateOnly.FromDateTime(value.Value) : null;
                OnPropertyChanged(); // Notify that this property changed
            }
        }
    }

    partial void OnSelectedVehicleChanged(vehicle? value)
    {
        if (value is not null) IsControls2Enabled = true;
        else IsControls2Enabled = false;
        OnPropertyChanged(nameof(LastServiceDateTime));
    }

    private async Task LoadData()
    {
        try
        {
            var vehiclesList = await _vehiclesService.GetAllVehicles();
            Vehicles.Clear();
            foreach (var vehicle in vehiclesList)
            {
                Vehicles.Add(vehicle);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }



    [RelayCommand]
    private void NewVehicle()
    {
        IsControlsEnabled = true;
        SelectedVehicle = new vehicle
        {
            brand = string.Empty,
            model = string.Empty,
            registration = string.Empty,
            notes = string.Empty,
            last_service = null
        };
    }


    [RelayCommand]
    private void CancelVehicle()
    {
        SelectedVehicle = null;
        IsControlsEnabled = false;
    }

    [RelayCommand]
    private void EditVehicle()
    {
        if (SelectedVehicle == null) return;
       isEditing = true; 
       IsControlsEnabled = true;
    }


    [RelayCommand]
    private async Task DeleteVehicle()
    {
        if (SelectedVehicle == null) return;
        
        try
        {
            await _vehiclesService.DeleteVehicle(SelectedVehicle);
            await LoadData();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    

    [RelayCommand]
    private async Task SaveVehicle()
    {
        if (SelectedVehicle == null) return;

        
        try
        {
            if (isEditing)
            {
                await _vehiclesService.UpdateVehicle(SelectedVehicle);
            }
            else
            {
                await _vehiclesService.CreateVehicle(SelectedVehicle);
                
            }

            await LoadData();
            IsControlsEnabled = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}