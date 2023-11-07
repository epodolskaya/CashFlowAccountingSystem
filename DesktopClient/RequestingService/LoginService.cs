using DesktopClient.Commands.Abstractions;
using DesktopClient.Commands.Login;
using DesktopClient.RequestingService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DesktopClient.RequestingService;
internal class LoginService : ILoginService
{
    private static readonly string ServerUrl;

    private static readonly HttpClient HttpClient;

    static LoginService()
    {
        ServerUrl = "https://localhost:7093";
        HttpClient = new HttpClient();
        HttpClient.Timeout = TimeSpan.FromSeconds(3);
    }

    public async Task SignInAsync(SignInCommand command)
    {
        HttpResponseMessage response = await HttpClient.PostAsync($"{ServerUrl}/Account/Login", new StringContent(JsonSerializer.Serialize(command)));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        JwtTokenVault.JwtToken = response.Headers.GetValues("Authorization").SingleOrDefault();
    }

    public async Task RegisterAsync(RegisterCommand command)
    {
        HttpResponseMessage response = await HttpClient.PostAsync
                                           ($"{ServerUrl}/Account/Register", new StringContent(JsonSerializer.Serialize(command)));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        JwtTokenVault.JwtToken = response.Headers.GetValues("Authorization").SingleOrDefault();
    }

    public async Task SignOutAsync()
    {
        HttpResponseMessage response = await HttpClient.GetAsync("/Register");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        JwtTokenVault.JwtToken = null;
    }
}