using LiveCharts;
using LiveCharts.Wpf;
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

namespace DesktopClient.Forms.FinancialAnalystWindows;
/// <summary>
/// Interaction logic for ProfitabilityChartWindow.xaml
/// </summary>
public partial class ProfitabilityChartWindow : Window
{
    public SeriesCollection Collection { get; set; }

    public ProfitabilityChartWindow(DateTime date)
    {
        Collection = new SeriesCollection()
        {
            new PieSeries()
            {
                Title = "T1",
                Values = new ChartValues<int>(){1},
                DataLabels = true
            },
            new PieSeries()
            {
                Title = "T2",
                Values = new ChartValues<int>(){2},
                DataLabels = true
            },
        };

        DataContext = this;
        InitializeComponent();
    }
}
