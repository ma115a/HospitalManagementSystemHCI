using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagementSystem.Nurse.Views;

namespace HospitalManagementSystem.LabWorker.Views;

public partial class LabWorkerResults : UserControl
{
    public LabWorkerResults()
    {
        InitializeComponent();
    }

    private void GoToDashboard_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as LabWindow;
        parentWindow?.GoToDashboard();
    }
    
    private void TimeBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        // allow only digits
        e.Handled = !e.Text.All(char.IsDigit);
    }

    private void TimeBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        // keep backspace/delete working normally
        if (e.Key == Key.Space) e.Handled = true; // no spaces
    }

    private void TimeBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;

        var caret = tb.CaretIndex;
        var digits = new string(tb.Text.Where(char.IsDigit).ToArray());

        if (digits.Length > 4) digits = digits.Substring(0, 4);

        string formatted;
        if (digits.Length <= 2)
            formatted = digits;                 // "1", "11"
        else
            formatted = digits.Insert(2, ":");  // "1130" -> "11:30"

        // only update if changed to avoid flicker/loops
        if (tb.Text != formatted)
        {
            tb.Text = formatted;

            // set caret smartly: after the char the user just typed
            // if we just inserted ':', move caret past it
            if (digits.Length == 3 && caret == 3) caret++; // typing the first minute adds ':'
            tb.CaretIndex = Math.Min(caret, tb.Text.Length);
        }
    }
    

 
}