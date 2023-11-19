using DesktopClient.Commands.Employee;
using DesktopClient.Constants;
using DesktopClient.Entity;
using DesktopClient.RequestingService;
using DesktopClient.RequestingService.Abstractions;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace DesktopClient.Forms.FinancialAnalystWindows;

/// <summary>
///     Interaction logic for CreateOrUpdateEmployeeWindow.xaml
/// </summary>
public partial class CreateOrUpdateEmployeeWindow : Window
{
    private readonly Employee _employee = new Employee();

    private readonly IRequestingService<Employee> _employeesService = new RequestingService<Employee>();

    private readonly List<Position> _positions = new List<Position>();
    private readonly IRequestingService<Position> _positionsService = new RequestingService<Position>();

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
        SalaryTextBox.Text = employee.Salary.ToString();
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
                CreateEmployeeCommand command = new CreateEmployeeCommand
                {
                    Name = NameTextBox.Text,
                    Surname = SurnameTextBox.Text,
                    DateOfBirth = DateOfBirthPicker.SelectedDate.Value,
                    PhoneNumber = PhoneNumberTextBox.Text,
                    Salary = value,
                    PositionId = ((Position)PositionsComboBox.SelectedItem).Id
                };

                await _employeesService.CreateAsync<CreateEmployeeCommand>(command);
            }
            else
            {
                UpdateEmployeeCommand command = new UpdateEmployeeCommand
                {
                    Id = _employee.Id,
                    Name = NameTextBox.Text,
                    Surname = SurnameTextBox.Text,
                    DateOfBirth = DateOfBirthPicker.SelectedDate.Value,
                    PhoneNumber = PhoneNumberTextBox.Text,
                    Salary = value,
                    PositionId = ((Position)PositionsComboBox.SelectedItem).Id
                };

                await _employeesService.UpdateAsync<UpdateEmployeeCommand>(command);
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);

            return;
        }

        Close();
    }

    private void SaveAccountButton_Click(object sender, RoutedEventArgs e) { }

    private async void CreateOrUpdateEmployeeWindow_OnInitialized(object? sender, EventArgs e)
    {
        await LoadData();

        PositionsComboBox.SelectedItem =
            PositionsComboBox.ItemsSource.Cast<Position>().SingleOrDefault(x => x.Id == _employee.PositionId);

        PositionsComboBox.Items.Refresh();
    }
}