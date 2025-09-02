using System.Windows;

namespace HospitalManagementSystem.Surgeon;

public partial class SurgeonWindow : Window
{
    public SurgeonWindow()
    {
        InitializeComponent();
    }

    public void GoToDashboard()
    {
        SurgeonSwitcher.SelectedIndex = 0;
    }
    
    public void GoToSurgery()
    {
        SurgeonSwitcher.SelectedIndex = 1;
    }

    public void GoToSurgeriesView()
    {
        SurgeonSwitcher.SelectedIndex = 2;
    }

    public void GoToSurgeriesHistory()
    {
        SurgeonSwitcher.SelectedIndex = 3;
    }
}