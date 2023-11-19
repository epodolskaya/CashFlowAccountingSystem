using DesktopClient.Commands.Operation;
using DesktopClient.Entity;
using DesktopClient.RequestingService;
using DesktopClient.RequestingService.Abstractions;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace DesktopClient.Forms.FinancialAnalystWindows;

/// <summary>
///     Interaction logic for CreateOrUpdateOperationWindow.xaml
/// </summary>
public partial class CreateOrUpdateOperationWindow : Window
{
    private readonly List<OperationCategory> _categories = new List<OperationCategory>();

    private readonly IRequestingService<OperationCategory> _categoriesService = new RequestingService<OperationCategory>();

    private readonly Operation _operation = new Operation();

    private readonly IRequestingService<Operation> _operationsService = new RequestingService<Operation>();

    private readonly List<OperationType> _types = new List<OperationType>();

    private readonly IRequestingService<OperationType> _typesService = new RequestingService<OperationType>();

    public CreateOrUpdateOperationWindow()
    {
        InitializeComponent();
        CategoryComboBox.ItemsSource = _categories;
        TypeComboBox.ItemsSource = _types;
    }

    public CreateOrUpdateOperationWindow(Operation operation) : this()
    {
        TitleLabel.Content = "Редактировать операцию";
        _operation = operation;
        CommentBox.Text = operation.Comment;
        SumBox.Text = operation.Sum.ToString();
        DatePicker.SelectedDate = operation.Date;
    }

    private async Task LoadData()
    {
        _categories.AddRange(await _categoriesService.GetAllAsync());
        _types.AddRange(await _typesService.GetAllAsync());
        TypeComboBox.Items.Refresh();
        CategoryComboBox.Items.Refresh();
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
            MessageBox.Show("Категория операции не выбрана!");

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

        try
        {
            if (_operation.Id == 0)
            {
                CreateOperationCommand command = new CreateOperationCommand
                {
                    CategoryId = ((OperationCategory)CategoryComboBox.SelectedItem).Id,
                    TypeId = ((OperationType)TypeComboBox.SelectedItem).Id,
                    Comment = CommentBox.Text,
                    Date = DatePicker.SelectedDate.Value,
                    Sum = sum
                };

                await _operationsService.CreateAsync<CreateOperationCommand>(command);
            }
            else
            {
                UpdateOperationCommand command = new UpdateOperationCommand
                {
                    Id = _operation.Id,
                    CategoryId = ((OperationCategory)CategoryComboBox.SelectedItem).Id,
                    TypeId = ((OperationType)TypeComboBox.SelectedItem).Id,
                    Comment = CommentBox.Text,
                    Date = DatePicker.SelectedDate.Value,
                    Sum = sum
                };

                await _operationsService.UpdateAsync<UpdateOperationCommand>(command);
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);

            return;
        }

        Close();
    }

    private async void CreateOrUpdateOperationWindow_OnInitialized(object? sender, EventArgs e)
    {
        await LoadData();

        TypeComboBox.SelectedItem =
            TypeComboBox.ItemsSource.Cast<OperationType>().SingleOrDefault(x => x.Id == _operation.TypeId);

        CategoryComboBox.SelectedItem = CategoryComboBox.ItemsSource.Cast<OperationCategory>()
                                                        .SingleOrDefault(x => x.Id == _operation.CategoryId);

        TypeComboBox.Items.Refresh();
        CategoryComboBox.Items.Refresh();
    }
}