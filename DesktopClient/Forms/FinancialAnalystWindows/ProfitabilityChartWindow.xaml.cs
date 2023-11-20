using DesktopClient.Entity;
using DesktopClient.RequestingService;
using DesktopClient.RequestingService.Abstractions;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;

namespace DesktopClient.Forms.FinancialAnalystWindows;

/// <summary>
///     Interaction logic for ProfitabilityChartWindow.xaml
/// </summary>
public partial class ProfitabilityChartWindow : Window
{
    private IRequestingService<Operation> _requestingService = new RequestingService<Operation>();

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

        /*{
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
        };*/

        DataContext = this;
        InitializeComponent();
    }

    public SeriesCollection Collection { get; set; }
}