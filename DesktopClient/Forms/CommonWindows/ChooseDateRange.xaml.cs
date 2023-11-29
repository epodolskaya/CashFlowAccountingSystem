using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace DesktopClient.Forms.FinancialAnalystWindows;

/// <summary>
///     Interaction logic for ChooseDateRange.xaml
/// </summary>
public partial class ChooseDateRange : Window
{
    public ChooseDateRange()
    {
        InitializeComponent();
    }

    public DateTime? DateFrom { get; private set; }

    public DateTime? DateTo { get; private set; }

    private void Apply_Click(object sender, RoutedEventArgs e)
    {
        if (!From.SelectedDate.HasValue)
        {
            MessageBox.Show("Начальная дата не выбрана!");

            return;
        }

        if (!To.SelectedDate.HasValue)
        {
            MessageBox.Show("Конечная дата не выбрана!");

            return;
        }

        if (To.SelectedDate.Value < From.SelectedDate.Value)
        {
            MessageBox.Show("Конечная дата не можеть быть меньше начальной!");

            return;
        }

        DateFrom = From.SelectedDate;
        DateTo = To.SelectedDate;
        Close();
    }
}