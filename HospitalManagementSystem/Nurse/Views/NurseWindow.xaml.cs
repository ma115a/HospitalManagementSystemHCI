using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using HospitalManagementSystem.LabWorker.ViewModels;
using HospitalManagementSystem.Nurse.ViewModels;
using HospitalManagementSystem.Utils;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Nurse.Views;

public partial class NurseWindow : Window
{
    
    
    
    private readonly LocalizationManager  _localizationManager;
    private readonly ThemeService _themeService;
    public NurseWindow(NurseWindowViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
        _localizationManager = App.HostApp.Services.GetRequiredService<LocalizationManager>();
        _themeService = App.HostApp.Services.GetRequiredService<ThemeService>();
    }

    public void GoToDashboard()
    {
        NurseSwitcher.SelectedIndex = 0;
    }

    public void GoToAppointments()
    {
        NurseSwitcher.SelectedIndex = 1;
    }

    public void GoToScheduleAppointment()
    {
        NurseSwitcher.SelectedIndex = 2;
    }

    public void GoToAdmissionsAndBeds()
    {
        NurseSwitcher.SelectedIndex = 3;
    }

    public void GoToLab()
    {
        NurseSwitcher.SelectedIndex = 4;
    }

    public void GoToSurgeries()
    {
        NurseSwitcher.SelectedIndex = 5;
    }

    public void GoToPatients()
    {
        NurseSwitcher.SelectedIndex = 6;
    }
    
    
    private void SettingsMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (sender is ContextMenu menu)
            {
                // Restore theme checkmark
                var savedTheme = Properties.Settings.Default.Theme;
                var themeMenu = (MenuItem)menu.Items[1]; // "🎨 Theme"
                foreach (MenuItem item in themeMenu.Items.OfType<MenuItem>())
                    item.IsChecked = (string)item.Tag == savedTheme;

                // Restore primary color checkmark
                var savedColor = Properties.Settings.Default.PrimaryColor;
                var colorMenu = (MenuItem)menu.Items[2]; // "🎨 Primary Color"
                foreach (MenuItem item in colorMenu.Items.OfType<MenuItem>())
                    item.IsChecked = (string)item.Tag == savedColor;

                // Restore language checkmark
                var savedLang = Properties.Settings.Default.Language;
                var langMenu = (MenuItem)menu.Items[0]; // "🌐 Language"
                foreach (MenuItem item in langMenu.Items.OfType<MenuItem>())
                    item.IsChecked = (string)item.Tag == savedLang;
            }
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.ContextMenu != null)
            {
                btn.ContextMenu.PlacementTarget = btn;
                btn.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom; 
                btn.ContextMenu.HorizontalOffset = -60; // move right
                btn.ContextMenu.VerticalOffset = 0;    // adjust down if needed
                btn.ContextMenu.IsOpen = true;
            }
        }
        private void LanguageMenu_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string cultureCode)
            {
                _localizationManager.CurrentCulture = new CultureInfo(cultureCode);
            }
        }
        private void ThemeMenu_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string themeName)
            {
                _themeService.SetTheme(themeName == "Light" ? BaseTheme.Light : BaseTheme.Dark);

                // Update checkmarks
                var parent = (MenuItem)menuItem.Parent;
                foreach (var item in parent.Items.OfType<MenuItem>())
                    item.IsChecked = false;
                menuItem.IsChecked = true;
            }
        }
        private void PrimaryColorMenu_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string hex)
            {
                _themeService.SetColorScheme(hex);

                // Update checkmarks
                var parent = (MenuItem)menuItem.Parent;
                foreach (var item in parent.Items.OfType<MenuItem>())
                    item.IsChecked = false;
                menuItem.IsChecked = true;
            }
        }
}