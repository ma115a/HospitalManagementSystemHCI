using System.Windows;

namespace HospitalManagementSystem;

public partial class LabWindow : Window
{
    public LabWindow()
    {
        InitializeComponent();
    }

    public void GoToDashboard()
    {
        LabSwitcher.SelectedIndex = 0;
    }

    public void GoToRequests()
    {
        LabSwitcher.SelectedIndex = 1;
    }

    public void GoToResults()
    {
        LabSwitcher.SelectedIndex = 2;
    }

    public void GoToHistory()
    {
        LabSwitcher.SelectedIndex = 3;
    }
    
}