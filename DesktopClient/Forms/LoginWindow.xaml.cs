using DesktopClient.Forms.EmployeeWindows;
using DesktopClient.RequestingServices;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace DesktopClient.Forms;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    private readonly AuthService _authService = new AuthService();

    public LoginWindow()
    {
        InitializeComponent();
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        if (UserName.Text.Length == 0 || Password.Password.Length == 0)
        {
            MessageBox.Show("Поля ввода не могут быть пустыми");
        }

        try
        {
            await _authService.SignInAsync(UserName.Text, Password.Password);

            switch (_authService.GetRole())
            {
                case Roles.Head:
                    {
                        new HeadWindows.MainWindow().Show();
                        Close();

                        break;
                    }
                case Roles.DepartmentHead:
                    {
                        new MainWindow().Show();
                        Close();

                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }
}