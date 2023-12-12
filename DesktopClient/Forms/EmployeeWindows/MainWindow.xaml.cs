using DesktopClient.Constants;
using DesktopClient.Entity;
using DesktopClient.Forms.CommonWindows;
using DesktopClient.Forms.HeadWindows;
using DesktopClient.RequestingServices;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace DesktopClient.Forms.EmployeeWindows;

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

    private void MyProfileButton_Click(object sender, RoutedEventArgs e)
    {
        MainTab.SelectedIndex = 2;
        NameTextBox.Text = _employee.Name;
        SurnameTextBox.Text = _employee.Surname;
        PhoneTextBox.Text = _employee.PhoneNumber;
    }

    private void FindByCategoryButton_Click(object sender, RoutedEventArgs e)
    {
        OperationCategory? selectedCategory = CategoriesComboBox.SelectedValue as OperationCategory;

        if (selectedCategory is null || selectedCategory.Id == 0)
        {
            MessageBox.Show("Статья не выбрана.");

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
        _operations.AddRange(allOperations.Where(x => x.Category.Type.Name == "Доходы"));
        OperationsGrid.Items.Refresh();
    }

    private async void OutcomsButton_Checked(object sender, RoutedEventArgs e)
    {
        ICollection<Operation> allOperations = await _operationService.GetByCurrentDepartmentAsync();
        _operations.Clear();
        _operations.AddRange(allOperations.Where(x => x.Category.Type.Name == "Расходы"));
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
        IReadOnlyCollection<Operation> selectedOperations = OperationsGrid.SelectedItems.Cast<Operation>().ToArray();

        if (selectedOperations.Count == 0)
        {
            MessageBox.Show("Сначала выберите операцию.");

            return;
        }

        await Task.WhenAll(selectedOperations.Select(x => _operationService.DeleteAsync(x.Id)));

        _operations.Clear();
        _operations.AddRange(await _operationService.GetByCurrentDepartmentAsync());
        OperationsGrid.Items.Refresh();
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
        IReadOnlyCollection<Employee> selectedEmployees = EmployeesGrid.SelectedItems.Cast<Employee>().ToArray();

        if (selectedEmployees.Count == 0)
        {
            MessageBox.Show("Сначала выберите сотрудника.");

            return;
        }

        await Task.WhenAll(selectedEmployees.Select(x => _employeesService.DeleteAsync(x.Id)));

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

        Employee employee = new Employee
        {
            Id = JwtTokenVault.EmployeeId,
            Name = NameTextBox.Text,
            Surname = SurnameTextBox.Text,
            PhoneNumber = PhoneTextBox.Text,
            Salary = _employee.Salary,
            PositionId = _employee.PositionId,
            DateOfBirth = _employee.DateOfBirth,
            DepartmentId = _employee.DepartmentId
        };

        await _employeesService.UpdateAsync(employee);

        _employee = await _employeesService.GetByIdAsync(JwtTokenVault.EmployeeId);
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

        await _authService.ChangePasswordAsync(OldPasswordBox.Password, NewPasswordBox.Password);
    }

    private async void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        await _authService.SignOutAsync();
        LoginWindow loginWindow = new LoginWindow();
        Close();
        loginWindow.ShowDialog();
    }

    private async void ProvidePayment_Click(object sender, RoutedEventArgs e)
    {
        IReadOnlyCollection<Employee> selectedEmployees = EmployeesGrid.SelectedItems.Cast<Employee>().ToArray();

        if (selectedEmployees.Count == 0)
        {
            MessageBoxResult dialogResult = MessageBox.Show
                ("Сотрудники не выбраны, начислить всем?", "Сотрудники не выбраны", MessageBoxButton.YesNo);

            if (dialogResult != MessageBoxResult.Yes)
            {
                return;
            }

            selectedEmployees = EmployeesGrid.Items.Cast<Employee>().ToList();
        }

        IEnumerable<(Operation salary, Operation tax)> operations = selectedEmployees.Select
            (x => (new Operation
                   {
                       CategoryId = _categories.Single(x => x.Name == "Выплата заработной платы").Id,
                       Comment = $"Выплата заработной платы сотруднику {x.Surname} {x.Name}",
                       Date = DateTime.Now,
                       DepartmentId = x.DepartmentId,
                       Sum = x.Salary
                   },
                   new Operation
                   {
                       CategoryId = _categories.Single(x => x.Name == "Налоги").Id,
                       Comment = $"Выплата налогов из заработной платы сотрудника {x.Surname} {x.Name}",
                       Date = DateTime.Now,
                       DepartmentId = x.DepartmentId,
                       Sum = x.Salary * (decimal)0.13
                   }
                  ));

        await Task.WhenAll
            (operations.SelectMany
                (x => Enumerable.Empty<Task>()
                                .Append(_operationService.CreateAsync(x.salary))
                                .Append(_operationService.CreateAsync(x.tax))));

        _operations.Clear();    
        _operations.AddRange(await _operationService.GetByCurrentDepartmentAsync());
        OperationsGrid.Items.Refresh();
    }

    private async void OperationsImport_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog dialog = new OpenFileDialog();

        if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
        {
            return;
        }

        string filename = dialog.FileName;

        IReadOnlyCollection<string> content = await File.ReadAllLinesAsync(filename);

        IEnumerable<Operation> operations = OperationsCsvSerializer.Deserialize(content).ToArray();

        _operations.AddRange(await Task.WhenAll(operations.Select(x => _operationService.CreateAsync(x))));

        OperationsGrid.Items.Refresh();
    }

    private void OperationsExport_Click(object sender, RoutedEventArgs e)
    {
        IReadOnlyCollection<Operation> selectedOperations = OperationsGrid.SelectedItems.Cast<Operation>().ToList();

        if (selectedOperations.Count == 0)
        {
            MessageBoxResult dialogResult = MessageBox.Show
                ("Операции не выбраны, экспортировать все?", "Операции не выбраны", MessageBoxButton.YesNo);

            if (dialogResult != MessageBoxResult.Yes)
            {
                return;
            }

            selectedOperations = OperationsGrid.Items.Cast<Operation>().ToList();
        }

        FolderBrowserDialog dialog = new FolderBrowserDialog();
        DialogResult result = dialog.ShowDialog();

        if (result != System.Windows.Forms.DialogResult.OK)
        {
            return;
        }

        string path = Path.Combine(dialog.SelectedPath, $"Экспортированные операции на {DateTime.Today:dd.MM.yyyy}.csv");

        IEnumerable<string> content = OperationsCsvSerializer.Serialize(selectedOperations);

        File.WriteAllLines(path, content, Encoding.UTF8);
    }

    private async void OperationsGrid_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
    {
        IReadOnlyCollection<Operation> selectedItems = OperationsGrid.SelectedItems.Cast<Operation>().ToArray();

        if (selectedItems.Count == 0)
        {
            return;
        }

        MessageBoxResult result = MessageBox.Show
            ("Вы действительно хотите отправить операции в другой отдел?", "Перемещение операций", MessageBoxButton.YesNo);

        if (result != MessageBoxResult.Yes)
        {
            return;
        }

        SelectDepartmentWindow selectionDepartmentWindow = new SelectDepartmentWindow();

        selectionDepartmentWindow.ShowDialog();

        if (selectionDepartmentWindow.SelectedDepartment is null)
        {
            return;
        }

        await Task.WhenAll
            (selectedItems
                .Select
                    (x =>
                    {
                        x.DepartmentId = selectionDepartmentWindow.SelectedDepartment.Id;

                        return _operationService.UpdateAsync(x);
                    }));

        _operations.RemoveAll(x => selectedItems.Contains(x));
        OperationsGrid.Items.Refresh();
    }
}