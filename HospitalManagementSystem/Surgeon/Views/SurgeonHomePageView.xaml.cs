using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Surgeon.Views;

public partial class SurgeonHomePageView : UserControl
{
    public SurgeonHomePageView()
    {
        InitializeComponent();
    }

    private void Surgeries_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as SurgeonWindow;
        parentWindow?.GoToSurgery();
    }

    private void ScheduleSurgery_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as SurgeonWindow;
        parentWindow?.GoToSurgeriesView();
    }

    private void SurgeriesHistory_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as SurgeonWindow;
        parentWindow?.GoToSurgeriesHistory();
    }
    
}