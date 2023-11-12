using DesktopClient.Entity;
using DesktopClient.RequestingService;
using DesktopClient.RequestingService.Abstractions;
using System.Windows;

namespace DesktopClient.Forms.FinancialAnalystWindows;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly List<OperationCategory> _categories = new List<OperationCategory>();

    private readonly List<Employee> _employees = new List<Employee>();

    private readonly IRequestingService<Employee> _employeesCategoriesService = new RequestingService<Employee>();

    private readonly IRequestingService<OperationCategory> _operationCategoriesService =
        new RequestingService<OperationCategory>();

    private readonly List<Operation> _operations = new List<Operation>();
    private readonly IRequestingService<Operation> _operationService = new RequestingService<Operation>();

    public MainWindow()
    {
        InitializeComponent();
        LoadData();
        Thread.Sleep(1000);
        CategoriesComboBox.ItemsSource = _categories;
        OperationsGrid.ItemsSource = _operations;
        EmployeesGrid.ItemsSource = _employees;
    }

    private async Task LoadData()
    {
        _operations.AddRange(await _operationService.GetAllAsync());

        _categories.AddRange(await _operationCategoriesService.GetAllAsync());

        _employees.AddRange(await _employeesCategoriesService.GetAllAsync());
    }

    private void MainPageButton_Click(object sender, RoutedEventArgs e)
    {
        MainTab.SelectedIndex = 0;
    }

    private void FindByDateButton_Click(object sender, RoutedEventArgs e)
    {
        DateTime? selectedDate = DatePicker.SelectedDate;

        if (selectedDate is null)
        {
            MessageBox.Show("Дата не выбрана.");

            return;
        }

        List<Operation> operationsWithSelectedDate = _operations.Where
                                                                    (operation =>
                                                                        DateOnly.FromDateTime(operation.Date) ==
                                                                        DateOnly.FromDateTime(selectedDate.Value))
                                                                .ToList();

        _operations.Clear();
        _operations.AddRange(operationsWithSelectedDate);
        OperationsGrid.Items.Refresh();
    }

    private void EmployeesButton_Click(object sender, RoutedEventArgs e)
    {
        MainTab.SelectedIndex = 1;
    }

    private void ReportsButton_Click(object sender, RoutedEventArgs e)
    {
        MainTab.SelectedIndex = 2;
    }

    private void MyProfileButton_Click(object sender, RoutedEventArgs e)
    {
        MainTab.SelectedIndex = 3;
    }

    private void FindByCategoryButton_Click(object sender, RoutedEventArgs e)
    {
        OperationCategory? selectedCategory = CategoriesComboBox.SelectedValue as OperationCategory;

        if (selectedCategory is null || selectedCategory.Id == 0)
        {
            MessageBox.Show("Категория не выбрана.");

            return;
        }

        List<Operation> operationsWithSelectedCategory = _operations.Where
                                                                        (operation =>
                                                                            operation.Category.Name == selectedCategory.Name)
                                                                    .ToList();

        _operations.Clear();
        _operations.AddRange(operationsWithSelectedCategory);
        OperationsGrid.Items.Refresh();
    }

    private async void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        _operations.Clear();
        _operations.AddRange(await _operationService.GetAllAsync());
        OperationsGrid.Items.Refresh();
        AllRadioButton.IsChecked = true;
    }

    private async void AllRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        ICollection<Operation> allOperations = await _operationService.GetAllAsync();
        _operations.Clear();
        _operations.AddRange(allOperations);
        OperationsGrid.Items.Refresh();
    }

    private async void IncomsRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        ICollection<Operation> allOperations = await _operationService.GetAllAsync();
        _operations.Clear();
        _operations.AddRange(allOperations.Where(x => x.Type.Name == "Доходы"));
        OperationsGrid.Items.Refresh();
    }

    private async void OutcomsButton_Checked(object sender, RoutedEventArgs e)
    {
        ICollection<Operation> allOperations = await _operationService.GetAllAsync();
        _operations.Clear();
        _operations.AddRange(allOperations.Where(x => x.Type.Name == "Расходы"));
        OperationsGrid.Items.Refresh();
    }

    private async void CreateOperation_Click(object sender, RoutedEventArgs e)
    {
        CreateOrUpdateOperationWindow window = new CreateOrUpdateOperationWindow();
        window.ShowDialog();
        _operations.Clear();
        _operations.AddRange(await _operationService.GetAllAsync());
        OperationsGrid.Items.Refresh();
    }

    private async void UpdateOperation_Click(object sender, RoutedEventArgs e)
    {
        Operation? selectedOperation = OperationsGrid.SelectedItem as Operation;

        if (selectedOperation is null)
        {
            MessageBox.Show("Сначала выберите операцию.");

            return;
        }

        CreateOrUpdateOperationWindow window = new CreateOrUpdateOperationWindow(selectedOperation);
        window.ShowDialog();
        _operations.Clear();
        _operations.AddRange(await _operationService.GetAllAsync());
        OperationsGrid.Items.Refresh();
    }

    private async void DeleteOperation_Click(object sender, RoutedEventArgs e)
    {
        Operation? selectedOperation = OperationsGrid.SelectedItem as Operation;

        if (selectedOperation is null)
        {
            MessageBox.Show("Сначала выберите операцию.");

            return;
        }

        try
        {
            await _operationService.DeleteAsync(selectedOperation.Id);
            _operations.Clear();
            _operations.AddRange(await _operationService.GetAllAsync());
            OperationsGrid.Items.Refresh();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }
}