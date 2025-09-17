



using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace HospitalManagementSystem.Utils;
using HospitalManagementSystem.Resources;



public class LocalizationManager : INotifyPropertyChanged
{

    private CultureInfo _currentCulture;

    public CultureInfo CurrentCulture
    {
        get => _currentCulture;
        set
        {
            if (_currentCulture != value)
            {
                _currentCulture = value;
                Thread.CurrentThread.CurrentCulture = value;
                Thread.CurrentThread.CurrentUICulture = value;
                CultureInfo.DefaultThreadCurrentCulture = value;
                CultureInfo.DefaultThreadCurrentUICulture = value;
                OnPropertyChanged(nameof(CurrentCulture));
            }
        }
    }
    
    
    public ResourceManager ResourceProvider { get; }

    public LocalizationManager()
    {
        ResourceProvider = Resources.Main.ResourceManager;
    }
    
    public string GetString(string key) => ResourceProvider.GetString(key, CurrentCulture);
    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    
}