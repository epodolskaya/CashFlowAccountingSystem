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
/// Interaction logic for ChooseDateWindow.xaml
/// </summary>
public partial class ChooseDateWindow : Window
{
    public DateTime? DateTime { get; private set; }
    public ChooseDateWindow()
    {
        InitializeComponent();
    }

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
