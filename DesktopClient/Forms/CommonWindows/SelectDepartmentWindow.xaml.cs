using DesktopClient.Entity;
using DesktopClient.RequestingServices;
using System.Windows;

namespace DesktopClient.Forms.CommonWindows;

/// <summary>
///     Interaction logic for SelectDepartmentWindow.xaml
/// </summary>
public partial class SelectDepartmentWindow : Window
{
    private readonly DepartmentsRequestingService _service = new DepartmentsRequestingService();

    public Department? SelectedDepartment { get; set; }

    public SelectDepartmentWindow()
    {
        InitializeComponent();
    }

    private async void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
    {
        DepartmentsBox.ItemsSource = (await _service.GetAllAsync()).Where(x => x.Id != JwtTokenVault.DepartmentId);
    }

    private void Apply_Click(object sender, RoutedEventArgs e)
    {
        SelectedDepartment = DepartmentsBox.SelectedItem as Department;
        Close();
    }
}