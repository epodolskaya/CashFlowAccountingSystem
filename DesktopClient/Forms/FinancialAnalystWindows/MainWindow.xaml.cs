using DesktopClient.Constants;
using DesktopClient.Entity;
using DesktopClient.RequestingServices;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace DesktopClient.Forms.FinancialAnalystWindows;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly AuthService _authService = new AuthService();
    private readonly List<OperationCategory> _categories = new List<OperationCategory>();

    private readonly List<Employee> _employees = new List<Employee>();

    private readonly EmployeesRequestingService _employeesService = new EmployeesRequestingService();

    private readonly OperationCategoriesRequestingService
        _operationCategoriesService = new OperationCategoriesRequestingService();

    private readonly List<Operation> _operations = new List<Operation>();

    private readonly OperationsRequestingService _operationService = new OperationsRequestingService();

    private Employee _employee;

    public MainWindow()
    {
        InitializeComponent();
        CategoriesComboBox.ItemsSource = _categories;
        OperationsGrid.ItemsSource = _operations;
        EmployeesGrid.ItemsSource = _employees;
    }

    private async Task LoadData()
    {
        _operations.AddRange(await _operationService.GetByCurrentDepartmentAsync());

        _categories.AddRange(await _operationCategoriesService.GetByCurrentDepartmentAsync());

        _employees.AddRange(await _employeesService.GetByCurrentDepartmentAsync());

        _employee = _employees.Single(x => x.Id == JwtTokenVault.EmployeeId);
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
        NameTextBox.Text = _employee.Name;
        SurnameTextBox.Text = _employee.Surname;
        PhoneTextBox.Text = _employee.PhoneNumber;
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
        _operations.AddRange(await _operationService.GetByCurrentDepartmentAsync());
        OperationsGrid.Items.Refresh();
        AllRadioButton.IsChecked = true;
    }

    private async void AllRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        ICollection<Operation> allOperations = await _operationService.GetByCurrentDepartmentAsync();
        _operations.Clear();
        _operations.AddRange(allOperations);
        OperationsGrid.Items.Refresh();
    }

    private async void IncomsRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        ICollection<Operation> allOperations = await _operationService.GetByCurrentDepartmentAsync();
        _operations.Clear();
        _operations.AddRange(allOperations.Where(x => x.Type.Name == "Доходы"));
        OperationsGrid.Items.Refresh();
    }

    private async void OutcomsButton_Checked(object sender, RoutedEventArgs e)
    {
        ICollection<Operation> allOperations = await _operationService.GetByCurrentDepartmentAsync();
        _operations.Clear();
        _operations.AddRange(allOperations.Where(x => x.Type.Name == "Расходы"));
        OperationsGrid.Items.Refresh();
    }

    private async void CreateOperation_Click(object sender, RoutedEventArgs e)
    {
        CreateOrUpdateOperationWindow window = new CreateOrUpdateOperationWindow();
        window.ShowDialog();
        _operations.Clear();
        _operations.AddRange(await _operationService.GetByCurrentDepartmentAsync());
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
        _operations.AddRange(await _operationService.GetByCurrentDepartmentAsync());
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
            _operations.AddRange(await _operationService.GetByCurrentDepartmentAsync());
            OperationsGrid.Items.Refresh();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }

    private async void MainWindow_OnInitialized(object? sender, EventArgs e)
    {
        await LoadData();
        CategoriesComboBox.Items.Refresh();
        OperationsGrid.Items.Refresh();
        EmployeesGrid.Items.Refresh();
    }

    private async void CreateEmployee_Click(object sender, RoutedEventArgs e)
    {
        CreateOrUpdateEmployeeWindow window = new CreateOrUpdateEmployeeWindow();
        window.ShowDialog();
        _employees.Clear();
        _employees.AddRange(await _employeesService.GetByCurrentDepartmentAsync());
        EmployeesGrid.Items.Refresh();
    }

    private async void UpdateEmployee_Click(object sender, RoutedEventArgs e)
    {
        Employee? selectedEmployee = EmployeesGrid.SelectedItem as Employee;

        if (selectedEmployee is null)
        {
            MessageBox.Show("Сначала выберите сотрудника.");

            return;
        }

        CreateOrUpdateEmployeeWindow window = new CreateOrUpdateEmployeeWindow(selectedEmployee);
        window.ShowDialog();
        _employees.Clear();
        _employees.AddRange(await _employeesService.GetByCurrentDepartmentAsync());
        EmployeesGrid.Items.Refresh();
    }

    private async void DeleteEmployee_Click(object sender, RoutedEventArgs e)
    {
        Employee? selectedEmployee = EmployeesGrid.SelectedItem as Employee;

        if (selectedEmployee is null)
        {
            MessageBox.Show("Сначала выберите сотрудника.");

            return;
        }

        await _employeesService.DeleteAsync(selectedEmployee.Id);
        _employees.Clear();
        _employees.AddRange(await _employeesService.GetByCurrentDepartmentAsync());
        EmployeesGrid.Items.Refresh();
    }

    private async void ApplyProfileData_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameTextBox.Text))
        {
            MessageBox.Show("Неверный формат имени.");

            return;
        }

        if (string.IsNullOrWhiteSpace(SurnameTextBox.Text))
        {
            MessageBox.Show("Неверный формат фамилии.");

            return;
        }

        if (!RegularExpressions.PhoneNumber.IsMatch(PhoneTextBox.Text))
        {
            MessageBox.Show("Неверный формат номера телефона.");

            return;
        }

        try
        {
            await _employeesService.UpdateAsync
                (new Employee
                {
                    Id = JwtTokenVault.EmployeeId,
                    Name = NameTextBox.Text,
                    Surname = SurnameTextBox.Text,
                    PhoneNumber = PhoneTextBox.Text,
                    Salary = _employee.Salary,
                    PositionId = _employee.PositionId,
                    DateOfBirth = _employee.DateOfBirth
                });

            _employee = await _employeesService.GetByIdAsync(JwtTokenVault.EmployeeId);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }

    private async void ApplyPasswordData_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(OldPasswordBox.Password))
        {
            MessageBox.Show("Старый пароль не может содержать пустые символы");
        }

        if (!RegularExpressions.AtLeastOneDigit.IsMatch(NewPasswordBox.Password))
        {
            MessageBox.Show("Пароль должен содержать хотя бы 1 цифру.");

            return;
        }

        if (!RegularExpressions.AtLeastOneLetter.IsMatch(NewPasswordBox.Password))
        {
            MessageBox.Show("Пароль должен содержать хотя бы 1 букву.");

            return;
        }

        if (!RegularExpressions.AtLeastOneLowercase.IsMatch(NewPasswordBox.Password))
        {
            MessageBox.Show("Пароль должен содержать хотя бы 1 букву нижнего регистра.");

            return;
        }

        if (!RegularExpressions.AtLeastOneSpecialCharacter.IsMatch(NewPasswordBox.Password))
        {
            MessageBox.Show("Пароль должен содержать хотя бы 1 специальный символ.");

            return;
        }

        if (!RegularExpressions.AtLeastOneUppercase.IsMatch(NewPasswordBox.Password))
        {
            MessageBox.Show("Пароль должен содержать хотя бы 1 букву верхнего регистра.");

            return;
        }

        try
        {
            await _authService.ChangePasswordAsync(OldPasswordBox.Password, NewPasswordBox.Password);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }

    private async void CreateIncomsAndOutcomsReport_Click(object sender, RoutedEventArgs e)
    {
        ChooseDateRange form = new ChooseDateRange();
        form.ShowDialog();

        if (!form.DateFrom.HasValue || !form.DateTo.HasValue)
        {
            return;
        }

        FolderBrowserDialog dialog = new FolderBrowserDialog();
        DialogResult result = dialog.ShowDialog();

        if (result == System.Windows.Forms.DialogResult.OK)
        {
            string path = dialog.SelectedPath;
            await ReportCreator.CreateIncomsAndOutcomsReport(path, form.DateFrom.Value, form.DateTo.Value);
        }
    }

    private async void CreateProfitabilityReport_Click(object sender, RoutedEventArgs e)
    {
        ChooseDateWindow form = new ChooseDateWindow();
        form.ShowDialog();

        if (!form.DateTime.HasValue)
        {
            return;
        }

        ICollection<Operation> operations = await _operationService.GetByCurrentDepartmentAsync();

        decimal sumOfIncoms = operations.Where
                                            (x => x.Date.Month == form.DateTime.Value.Month &&
                                                  x.Type.Name == "Доходы")
                                        .Sum(x => x.Sum);

        decimal taxes = operations.Where
                                      (x => x.Date.Month == form.DateTime.Value.Month &&
                                            x.Category.Name == "Налоги")
                                  .Sum(x => x.Sum);

        decimal clearSumOfIncoms = sumOfIncoms - taxes;

        if (sumOfIncoms == 0)
        {
            sumOfIncoms = 1;
        }

        decimal profitability = clearSumOfIncoms / sumOfIncoms * 100;

        MessageBox.Show($"Рентабельность {form.DateTime.Value:MM.yyyy} составила: {Math.Round(profitability, 2)}%");
    }

    private async void CreateIncomsAndOutcomsChart_Click(object sender, RoutedEventArgs e)
    {
        ChooseDateRange chooseDateRangeWindow = new ChooseDateRange();
        chooseDateRangeWindow.ShowDialog();

        if (!chooseDateRangeWindow.DateFrom.HasValue || !chooseDateRangeWindow.DateTo.HasValue)
        {
            return;
        }

        IEnumerable<Operation> allOperations = (await _operationService.GetByCurrentDepartmentAsync()).Where
            (x => x.Date >= chooseDateRangeWindow.DateFrom && x.Date <= chooseDateRangeWindow.DateTo);

        ILookup<string, decimal> incomsSumsByCategories = allOperations.Where(x => x.Type.Name == "Доходы")
                                                                       .ToLookup(x => x.Category.Name, x => x.Sum);

        ProfitabilityChartWindow form = new ProfitabilityChartWindow(incomsSumsByCategories);

        form.Show();
    }

    private async void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        await _authService.SignOutAsync();
        LoginWindow loginWindow = new LoginWindow();
        Close();
        loginWindow.ShowDialog();
    }
}