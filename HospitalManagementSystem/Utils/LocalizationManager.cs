



// using System.ComponentModel;
// using System.Globalization;
// using System.Resources;
//
// namespace HospitalManagementSystem.Utils;
// using HospitalManagementSystem.Resources;
//
//
//
// public class LocalizationManager : INotifyPropertyChanged
// {
//
//     private CultureInfo _currentCulture;
//
//     public CultureInfo CurrentCulture
//     {
//         get => _currentCulture;
//         set
//         {
//             if (_currentCulture != value)
//             {
//                 _currentCulture = value;
//                 Thread.CurrentThread.CurrentCulture = value;
//                 Thread.CurrentThread.CurrentUICulture = value;
//                 CultureInfo.DefaultThreadCurrentCulture = value;
//                 CultureInfo.DefaultThreadCurrentUICulture = value;
//                 OnPropertyChanged(nameof(CurrentCulture));
//             }
//         }
//     }
//     
//     
//     public ResourceManager ResourceProvider { get; }
//
//     public LocalizationManager()
//     {
//         ResourceProvider = Resources.Main.ResourceManager;
//         CurrentCulture = new CultureInfo(Properties.Settings.Default.Language);
//     }
//     
//     public string GetString(string key) => ResourceProvider.GetString(key, CurrentCulture);
//     public event PropertyChangedEventHandler PropertyChanged;
//
//     private void OnPropertyChanged(string name)
//     {
//         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
//         Properties.Settings.Default.Language = name;
//         Properties.Settings.Default.Save();
//         
//     }
//     
// }
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace HospitalManagementSystem.Utils
{
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
            ResourceProvider = HospitalManagementSystem.Resources.Main.ResourceManager;
            
            // Fix: Safely get the saved language with fallback
            var savedLanguage = Properties.Settings.Default.Language;
            CultureInfo initialCulture;
            
            try
            {
                // Try to create culture from saved setting
                if (!string.IsNullOrEmpty(savedLanguage))
                {
                    initialCulture = new CultureInfo(savedLanguage);
                }
                else
                {
                    // Use system default if no saved language
                    initialCulture = CultureInfo.CurrentCulture;
                }
            }
            catch (CultureNotFoundException)
            {
                // Fallback to English if saved culture is invalid
                initialCulture = new CultureInfo("en");
            }
            
            CurrentCulture = initialCulture;
        }

        public string GetString(string key) => ResourceProvider.GetString(key, CurrentCulture);

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
            // Only save language setting when CurrentCulture changes, not for other properties
            if (propertyName == nameof(CurrentCulture))
            {
                Properties.Settings.Default.Language = CurrentCulture.TwoLetterISOLanguageName;
                Properties.Settings.Default.Save();
            }
        }
        
        // Method to change language programmatically
        public void SetCulture(string cultureName)
        {
            try
            {
                var culture = new CultureInfo(cultureName);
                CurrentCulture = culture;
            }
            catch (CultureNotFoundException ex)
            {
                throw new ArgumentException($"Culture '{cultureName}' is not supported.", nameof(cultureName), ex);
            }
        }
    }
}