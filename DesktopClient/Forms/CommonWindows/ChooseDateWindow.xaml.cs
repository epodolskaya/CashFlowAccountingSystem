using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace DesktopClient.Forms.CommonWindows;

/// <summary>
///     Interaction logic for ChooseDateWindow.xaml
/// </summary>
public partial class ChooseDateWindow : Window
{
    public ChooseDateWindow()
    {
        InitializeComponent();
    }

    public DateTime? DateTime { get; private set; }

    private void Apply_Click(object sender, RoutedEventArgs e)
    {
        if (!DatePicker.SelectedDate.HasValue)
        {
            MessageBox.Show("Дата не выбрана!");

            return;
        }

        DateTime = DatePicker.SelectedDate;
        Close();
    }
}