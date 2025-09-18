using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Media;

namespace HospitalManagementSystem
{
    public class ThemeService
    {
        public  ThemeService()
        {
            
            var theme = Properties.Settings.Default.Theme;
            if (theme == "Light") SetTheme(BaseTheme.Light);
            if (theme == "Dark") SetTheme(BaseTheme.Dark);
            var pc = Properties.Settings.Default.PrimaryColor;

            SetColorScheme(pc);
        }
        public void SetTheme(BaseTheme baseTheme)
        {
            if (Application.Current.Resources.GetTheme() is Theme theme)
            {
                theme.SetBaseTheme(baseTheme);
                Application.Current.Resources.SetTheme(theme);
                Console.WriteLine("set theme called");
                Properties.Settings.Default.Theme = theme.GetBaseTheme().ToString();
                Properties.Settings.Default.Save();
            }
        }

        public void SetColorScheme(string hex)
        {
            if (Application.Current.Resources.GetTheme() is Theme theme)
            {
                var primaryColor = (Color)ColorConverter.ConvertFromString(hex);
                theme.SetPrimaryColor(primaryColor);
                Application.Current.Resources.SetTheme(theme);
                
                Properties.Settings.Default.PrimaryColor = hex;
                Properties.Settings.Default.Save();
            }
        }

        public BaseTheme GetCurrentBaseTheme()
        {
            return Application.Current.Resources.GetTheme()?.GetBaseTheme() ?? BaseTheme.Light;
        }
    }
}