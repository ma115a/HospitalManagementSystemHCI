using System.Windows;
using System.Windows.Controls;
using HospitalManagementSystem.Admin.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Admin.Views;

public partial class UserProfilesView : UserControl
{
    public UserProfilesView()
    {
        InitializeComponent();
        DataContext = App.HostApp.Services.GetRequiredService<UserProfilesViewModel>();
    }


    private void GoToDashboard(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as AdminWindow;
        parentWindow?.GoToDashboard();
    }
}