using DesktopClient.Commands.Employee;
using DesktopClient.Constants;
using DesktopClient.Entity;
using DesktopClient.RequestingService;
using DesktopClient.RequestingService.Abstractions;
using System.Text.RegularExpressions;
using System.Windows;

namespace DesktopClient.Forms.FinancialAnalystWindows;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly List<OperationCategory> _categories = new List<OperationCategory>();

    private readonly List<Employee> _employees = new List<Employee>();

    private readonly List<Operation> _operations = new List<Operation>();

    private Employee _employee;

    private readonly IRequestingService<Employee> _employeesService = new RequestingService<Employee>();

    private readonly IRequestingService<OperationCategory> _operationCategoriesService =
        new RequestingService<OperationCategory>();

    private readonly IRequestingService<Operation> _operationService = new RequestingService<Operation>();

    private readonly ILoginService _loginService = new LoginService();
    
    public MainWindow()
    {
        InitializeComponent();
        CategoriesComboBox.ItemsSource = _categories;
        OperationsGrid.ItemsSource = _operations;
        EmployeesGrid.ItemsSource = _employees;
    }

    private async Task LoadData()
    {
        _operations.AddRange(await _operationService.GetAllAsync());

        _categories.AddRange(await _operationCategoriesService.GetAllAsync());

        _employees.AddRange(await _employeesService.GetAllAsync());

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
        _employees.AddRange(await _employeesService.GetAllAsync());
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
        _employees.AddRange(await _employeesService.GetAllAsync());
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
        _employees.AddRange(await _employeesService.GetAllAsync());
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
            await _employeesService.UpdateAsync<UpdateEmployeeCommand>
                (new UpdateEmployeeCommand()
                {
                    Id = JwtTokenVault.EmployeeId,
                    Name = NameTextBox.Text,
                    Surname = SurnameTextBox.Text,
                    PhoneNumber = PhoneTextBox.Text,
                    Salary = _employee.Salary,
                    PositionId = _employee.PositionId,
                    DateOfBirth = _employee.DateOfBirth,
                });
            _employee = await _employeesService.GetByIdAsync(JwtTokenVault.EmployeeId);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
            return;
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
            await _loginService.ChangePasswordAsync(OldPasswordBox.Password, NewPasswordBox.Password);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
            return;
        }
    }
}