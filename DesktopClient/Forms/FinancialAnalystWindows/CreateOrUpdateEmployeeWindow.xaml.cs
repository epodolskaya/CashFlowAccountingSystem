using DesktopClient.Constants;
using DesktopClient.Entity;
using DesktopClient.RequestingServices;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace DesktopClient.Forms.FinancialAnalystWindows;

/// <summary>
///     Interaction logic for CreateOrUpdateEmployeeWindow.xaml
/// </summary>
public partial class CreateOrUpdateEmployeeWindow : Window
{
    private readonly AuthService _authService = new AuthService();

    private readonly EmployeesRequestingService _employeesService = new EmployeesRequestingService();

    private readonly List<Position> _positions = new List<Position>();

    private readonly PositionsRequestingService _positionsService = new PositionsRequestingService();
    private Employee _employee = new Employee();

    public CreateOrUpdateEmployeeWindow()
    {
        InitializeComponent();
        PositionsComboBox.ItemsSource = _positions;
    }

    public CreateOrUpdateEmployeeWindow(Employee employee) : this()
    {
        _employee = employee;
        NameTextBox.Text = employee.Name;
        SurnameTextBox.Text = employee.Surname;
        PhoneNumberTextBox.Text = employee.PhoneNumber;
        SalaryTextBox.Text = Math.Round(employee.Salary, 2).ToString();
        DateOfBirthPicker.SelectedDate = employee.DateOfBirth;
    }

    private async Task LoadData()
    {
        _positions.AddRange(await _positionsService.GetAllAsync());
        PositionsComboBox.Items.Refresh();
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (NameTextBox.Text.Length == 0)
        {
            MessageBox.Show("Имя должно быть заполнено");

            return;
        }

        if (SurnameTextBox.Text.Length == 0)
        {
            MessageBox.Show("Фамилия должна быть заполнена");

            return;
        }

        if (!DateOfBirthPicker.SelectedDate.HasValue)
        {
            MessageBox.Show("Дата должна быть выбрана");

            return;
        }

        if (PhoneNumberTextBox.Text.Length == 0)
        {
            MessageBox.Show("Номер должен быть заполнен");

            return;
        }

        if (!RegularExpressions.PhoneNumber.IsMatch(PhoneNumberTextBox.Text))
        {
            MessageBox.Show("Неверный формат номера");

            return;
        }

        if (!decimal.TryParse(SalaryTextBox.Text, out decimal value) || value < 0)
        {
            MessageBox.Show("Неверное значение зарплаты");

            return;
        }

        if (PositionsComboBox.SelectedItem is null)
        {
            MessageBox.Show("Позиция не выбрана");

            return;
        }

        try
        {
            if (_employee.Id == 0)
            {
                Employee employee = new Employee
                {
                    Name = NameTextBox.Text,
                    Surname = SurnameTextBox.Text,
                    DateOfBirth = DateOfBirthPicker.SelectedDate.Value,
                    PhoneNumber = PhoneNumberTextBox.Text,
                    Salary = value,
                    PositionId = ((Position)PositionsComboBox.SelectedItem).Id,
                    DepartmentId = JwtTokenVault.DepartmentId
                };

                _employee = await _employeesService.CreateAsync(employee);
            }
            else
            {
                Employee employee = new Employee
                {
                    Id = _employee.Id,
                    Name = NameTextBox.Text,
                    Surname = SurnameTextBox.Text,
                    DateOfBirth = DateOfBirthPicker.SelectedDate.Value,
                    PhoneNumber = PhoneNumberTextBox.Text,
                    Salary = value,
                    PositionId = ((Position)PositionsComboBox.SelectedItem).Id,
                    DepartmentId = JwtTokenVault.DepartmentId
                };

                await _employeesService.UpdateAsync(employee);
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);

            return;
        }

        Close();
    }

    private async void SaveAccountButton_Click(object sender, RoutedEventArgs e)
    {
        if (_employee.Id is 0)
        {
            MessageBox.Show("Сначала создайте сотрудника.");

            return;
        }

        if (string.IsNullOrEmpty(LoginBox.Text))
        {
            MessageBox.Show("Имя пользователя не может содержать пустые символы");

            return;
        }

        if (string.IsNullOrWhiteSpace(PasswordBox.Password))
        {
            MessageBox.Show("Старый пароль не может содержать пустые символы");
        }

        if (!RegularExpressions.AtLeastOneDigit.IsMatch(PasswordBox.Password))
        {
            MessageBox.Show("Пароль должен содержать хотя бы 1 цифру.");

            return;
        }

        if (!RegularExpressions.AtLeastOneLetter.IsMatch(PasswordBox.Password))
        {
            MessageBox.Show("Пароль должен содержать хотя бы 1 букву.");

            return;
        }

        if (!RegularExpressions.AtLeastOneLowercase.IsMatch(PasswordBox.Password))
        {
            MessageBox.Show("Пароль должен содержать хотя бы 1 букву нижнего регистра.");

            return;
        }

        if (!RegularExpressions.AtLeastOneSpecialCharacter.IsMatch(PasswordBox.Password))
        {
            MessageBox.Show("Пароль должен содержать хотя бы 1 специальный символ.");

            return;
        }

        if (!RegularExpressions.AtLeastOneUppercase.IsMatch(PasswordBox.Password))
        {
            MessageBox.Show("Пароль должен содержать хотя бы 1 букву верхнего регистра.");

            return;
        }

        try
        {
            await _authService.RegisterAsync(LoginBox.Text, PasswordBox.Password, _employee.Id);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private async void CreateOrUpdateEmployeeWindow_OnInitialized(object? sender, EventArgs e)
    {
        await LoadData();

        PositionsComboBox.SelectedItem =
            PositionsComboBox.ItemsSource.Cast<Position>().SingleOrDefault(x => x.Id == _employee.PositionId);

        PositionsComboBox.Items.Refresh();
    }
}