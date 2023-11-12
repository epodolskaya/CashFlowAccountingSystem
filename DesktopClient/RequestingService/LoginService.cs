using DesktopClient.Commands.Login;
using DesktopClient.RequestingService.Abstractions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DesktopClient.RequestingService;

internal class LoginService : ILoginService
{
    private static readonly string ServerUrl;

    private static readonly HttpClient HttpClient;

    private static Roles _role;

    static LoginService()
    {
        ServerUrl = "https://localhost:7093";
        HttpClient = new HttpClient();
        HttpClient.Timeout = TimeSpan.FromSeconds(3);
        _role = Roles.Unauthorized;
    }

    public Roles GetRole()
    {
        return _role;
    }

    public async Task SignInAsync(SignInCommand command)
    {
        HttpResponseMessage response = await HttpClient.PostAsync
                                           ($"{ServerUrl}/Account/Login",
                                            new StringContent
                                                (JsonSerializer.Serialize(command),
                                                 new MediaTypeHeaderValue("application/json")));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        JwtTokenVault.SetToken(response.Headers.GetValues("Authorization").SingleOrDefault());
        _role = JwtTokenVault.Role;
    }

    public async Task RegisterAsync(RegisterCommand command)
    {
        HttpResponseMessage response = await HttpClient.PostAsync
                                           ($"{ServerUrl}/Account/Register",
                                            new StringContent
                                                (JsonSerializer.Serialize(command),
                                                 new MediaTypeHeaderValue("application/json")));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        JwtTokenVault.SetToken(response.Headers.GetValues("Authorization").SingleOrDefault());
        _role = JwtTokenVault.Role;
    }

    public async Task SignOutAsync()
    {
        HttpResponseMessage response = await HttpClient.GetAsync($"{ServerUrl}/Account/Register");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        JwtTokenVault.SetToken(null);
        _role = Roles.Unauthorized;
    }
}