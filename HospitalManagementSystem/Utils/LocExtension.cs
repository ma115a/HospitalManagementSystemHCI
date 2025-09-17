








using System.Globalization;
using System.Windows.Markup;
using HospitalManagementSystem;
using HospitalManagementSystem.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Utils;
public class LocExtension : MarkupExtension
{
    public string Key { get; set; }
    public LocalizationManager Localization { get; set; }

    public LocExtension(string key)
    {
        Key = key;
        Localization = App.HostApp.Services.GetRequiredService<LocalizationManager>();
        Localization.CurrentCulture = new CultureInfo("sr");
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
        => Localization.GetString(Key);
}