using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace DesktopClient.Forms.FinancialAnalystWindows;
/// <summary>
/// Interaction logic for ChooseDateRange.xaml
/// </summary>
public partial class ChooseDateRange : Window
{
    public DateTime? DateFrom { get; private set; }

    public DateTime? DateTo { get; private set; }

    public ChooseDateRange()
    {
        InitializeComponent();
    }

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
