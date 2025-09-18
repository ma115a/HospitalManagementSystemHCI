


// using System;
// using System.Windows;
// using System.Windows.Data;
// using System.Windows.Markup;
// using HospitalManagementSystem.Utils;
// using Microsoft.Extensions.DependencyInjection;
//
// namespace HospitalManagementSystem.Utils
// {
//     public class LocExtension : MarkupExtension
//     {
//         public string Key { get; set; }
//
//         private readonly LocalizationManager _localization;
//
//         public LocExtension(string key)
//         {
//             Key = key;
//             _localization = App.HostApp.Services.GetRequiredService<LocalizationManager>();
//         }
//
//         public override object ProvideValue(IServiceProvider serviceProvider)
//         {
//             // Instead of returning a raw string, we bind to the LocalizationManager
//             var binding = new Binding("CurrentCulture")
//             {
//                 Source = _localization,
//                 Mode = BindingMode.OneWay,
//                 Converter = new LocValueConverter(Key, _localization)
//             };
//
//             return binding.ProvideValue(serviceProvider);
//         }
//     }
//
//     public class LocValueConverter : IValueConverter
//     {
//         private readonly string _key;
//         private readonly LocalizationManager _localization;
//
//         public LocValueConverter(string key, LocalizationManager localization)
//         {
//             _key = key;
//             _localization = localization;
//         }
//
//         public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
//         {
//             return _localization.GetString(_key) ?? $"!{_key}!";
//         }
//
//         public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
//         {
//             throw new NotImplementedException();
//         }
//     }
// }


using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Utils
{
    public class LocExtension : MarkupExtension
    {
        public string Key { get; set; }
        private readonly LocalizationManager _localization;

        public LocExtension(string key)
        {
            Key = key;
            _localization = App.HostApp.Services.GetRequiredService<LocalizationManager>();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // Instead of returning a raw string, we bind to the LocalizationManager
            var binding = new Binding("CurrentCulture")
            {
                Source = _localization,
                Mode = BindingMode.OneWay,
                Converter = new LocValueConverter(Key, _localization)
            };
            return binding.ProvideValue(serviceProvider);
        }
    }

    public class LocValueConverter : IValueConverter
    {
        private readonly string _key;
        private readonly LocalizationManager _localization;

        public LocValueConverter(string key, LocalizationManager localization)
        {
            _key = key;
            _localization = localization;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Fixed: Removed the asterisks which were causing syntax errors
            return _localization.GetString(_key) ?? $"!{_key}!";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
