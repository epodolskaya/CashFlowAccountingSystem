using DesktopClient.Commands.Login;
using DesktopClient.Entity;
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
    
    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        if (UserName.Text.Length == 0 || Password.Password.Length == 0)
        {
            MessageBox.Show("Поля ввода не могут быть пустыми");
        }

        try
        {
            _loginService.SignInAsync
                (new SignInCommand()
                {
                    UserName = UserName.Text,
                    Password = Password.Password
                });
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
            return;
        }
    }
}