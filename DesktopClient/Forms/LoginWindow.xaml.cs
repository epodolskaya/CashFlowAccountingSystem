using DesktopClient.Commands.Login;
using DesktopClient.Forms.FinancialAnalystWindows;
using DesktopClient.RequestingService;
using DesktopClient.RequestingService.Abstractions;
using System.Windows;

namespace DesktopClient.Forms;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    private readonly ILoginService _loginService = new LoginService();

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
            await _loginService.SignInAsync
                (new SignInCommand
                {
                    UserName = UserName.Text,
                    Password = Password.Password
                });

            switch (_loginService.GetRole())
            {
                case Roles.FinancialAnalyst:
                    {
                        new MainWindow().Show();
                        Close();

                        break;
                    }
                case Roles.DepartmentHead:
                    {
                        new DepartmentHeadWindows.MainWindow().Show();
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