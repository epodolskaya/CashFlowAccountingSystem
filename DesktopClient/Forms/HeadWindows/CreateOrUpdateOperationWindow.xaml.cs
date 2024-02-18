using DesktopClient.Entity;
using DesktopClient.RequestingServices;
using System.Windows;
using System.Windows.Controls;
using MessageBox = System.Windows.MessageBox;

namespace DesktopClient.Forms.HeadWindows;

/// <summary>
///     Interaction logic for CreateOrUpdateOperationWindow.xaml
/// </summary>
public partial class CreateOrUpdateOperationWindow : Window
{
    private readonly List<OperationCategory> _categories = new List<OperationCategory>();

    private readonly OperationCategoriesRequestingService _categoriesService = new OperationCategoriesRequestingService();

    private readonly List<Department> _departments = new List<Department>();

    private readonly DepartmentsRequestingService _departmentsRequestingService = new DepartmentsRequestingService();

    private readonly Operation _operation = new Operation
    {
        Category = new OperationCategory()
    };

    private readonly OperationsRequestingService _operationsService = new OperationsRequestingService();

    private readonly List<OperationType> _types = new List<OperationType>();

    private readonly OperationTypesRequestingService _typesService = new OperationTypesRequestingService();

    public CreateOrUpdateOperationWindow()
    {
        InitializeComponent();
        CategoryComboBox.ItemsSource = _categories;
        TypeComboBox.ItemsSource = _types;
        DepartmentComboBox.ItemsSource = _departments;
    }

    public CreateOrUpdateOperationWindow(Operation operation) : this()
    {
        TitleLabel.Content = "Редактировать операцию";
        _operation = operation;
        CommentBox.Text = operation.Comment;
        SumBox.Text = Math.Round(operation.Sum, 2).ToString();
        DatePicker.SelectedDate = operation.Date;
    }

    private async Task LoadData()
    {
        _categories.AddRange(await _categoriesService.GetAllAsync());
        _types.AddRange(await _typesService.GetAllAsync());
        _departments.AddRange(await _departmentsRequestingService.GetAllAsync());
        TypeComboBox.Items.Refresh();
        CategoryComboBox.Items.Refresh();
        DepartmentComboBox.Items.Refresh();
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (TypeComboBox.SelectedItem is null)
        {
            MessageBox.Show("Тип операции не выбран!");

            return;
        }

        if (CategoryComboBox.SelectedItem is null)
        {
            MessageBox.Show("Статья операции не выбрана!");

            return;
        }

        if (DepartmentComboBox.SelectedItem is null)
        {
            MessageBox.Show("Отдел не выбран!");

            return;
        }

        if (decimal.TryParse(SumBox.Text, out decimal sum) && sum > 0)
        {
            _operation.Sum = sum;
        }
        else
        {
            MessageBox.Show("Неверное значение суммы!");

            return;
        }

        if (!DatePicker.SelectedDate.HasValue)
        {
            MessageBox.Show("Дата не выбрана!");

            return;
        }

        _operation.Date = DatePicker.SelectedDate.Value;

        Operation operation = new Operation
        {
            Id = _operation.Id,
            CategoryId = ((OperationCategory)CategoryComboBox.SelectedItem).Id,
            Comment = CommentBox.Text,
            Date = DatePicker.SelectedDate.Value,
            Sum = sum,
            DepartmentId = ((Department)DepartmentComboBox.SelectedItem).Id
        };

        _ = _operation.Id == 0
                ? await _operationsService.CreateAsync(operation)
                : await _operationsService.UpdateAsync(operation);

        Close();
    }

    private async void CreateOrUpdateOperationWindow_OnInitialized(object? sender, EventArgs e)
    {
        await LoadData();

        TypeComboBox.SelectedItem =
            TypeComboBox.ItemsSource.Cast<OperationType>().SingleOrDefault(x => x.Id == _operation.Category.TypeId);

        CategoryComboBox.SelectedItem = CategoryComboBox.ItemsSource.Cast<OperationCategory>()
                                                        .SingleOrDefault(x => x.Id == _operation.CategoryId);

        DepartmentComboBox.SelectedItem = DepartmentComboBox.ItemsSource.Cast<Department>()
                                                            .SingleOrDefault(x => x.Id == _operation.DepartmentId);

        TypeComboBox.Items.Refresh();
        CategoryComboBox.Items.Refresh();
        DepartmentComboBox.Items.Refresh();
    }

    private void TypeComboBox_OnSelected(object sender, SelectionChangedEventArgs e)
    {
        OperationType operationType = (OperationType)TypeComboBox.SelectedItem;
        CategoryComboBox.SelectedItem = null;
        CategoryComboBox.ItemsSource = _categories.Where(x => x.TypeId == operationType.Id).ToList();
        CategoryComboBox.Items.Refresh();
    }
}