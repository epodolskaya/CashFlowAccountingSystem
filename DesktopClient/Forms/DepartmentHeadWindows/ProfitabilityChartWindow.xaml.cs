using DesktopClient.RequestingServices;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;

namespace DesktopClient.Forms.DepartmentHeadWindows;

/// <summary>
///     Interaction logic for ProfitabilityChartWindow.xaml
/// </summary>
public partial class ProfitabilityChartWindow : Window
{
    private OperationsRequestingService _requestingService = new OperationsRequestingService();

    public ProfitabilityChartWindow(ILookup<string, decimal> incomsSumsByCategories)
    {
        Collection = new SeriesCollection();

        foreach (IGrouping<string, decimal> group in incomsSumsByCategories)
        {
            Collection.Add
                (new PieSeries
                {
                    Title = group.Key,
                    Values = new ChartValues<decimal>
                    {
                        group.Sum()
                    },
                    DataLabels = true
                });
        }

        DataContext = this;
        InitializeComponent();
    }

    public SeriesCollection Collection { get; set; }
}