using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DesktopClient.RequestingServices;

internal class AuthService
{
    private static readonly string ServerUrl;

    private static readonly HttpClient HttpClient;

    private static Roles _role;

    static AuthService()
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

    public async Task SignInAsync(string userName, string password)
    {
        HttpResponseMessage response = await HttpClient.PostAsync
                                           ($"{ServerUrl}/Account/Login",
                                            new StringContent
                                                (JsonSerializer.Serialize
                                                     (new
                                                     {
                                                         UserName = userName,
                                                         Password = password
                                                     }),
                                                 new MediaTypeHeaderValue("application/json")));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        JwtTokenVault.SetToken(response.Headers.GetValues("Authorization").SingleOrDefault());
        _role = JwtTokenVault.Role;
    }

    public async Task RegisterAsync(string userName, string password, long employeeId)
    {
        HttpResponseMessage response = await HttpClient.PostAsync
                                           ($"{ServerUrl}/Account/Register",
                                            new StringContent
                                                (JsonSerializer.Serialize(new
                                                 {
                                                     Email = userName,
                                                     Password = password,
                                                     ConfirmPassword = password,
                                                     EmployeeId = employeeId
                                                 }),
                                                 new MediaTypeHeaderValue("application/json")));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task SignOutAsync()
    {
        HttpResponseMessage response = await HttpClient.GetAsync($"{ServerUrl}/Account/SignOut");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        JwtTokenVault.SetToken(null);
        _role = Roles.Unauthorized;
    }

    public async Task ChangePasswordAsync(string oldPassword, string newPassword)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);

        HttpResponseMessage response = await HttpClient.PostAsync
                                           ($"{ServerUrl}/Account/ChangePassword",
                                            new StringContent
                                                (JsonSerializer.Serialize
                                                     (new
                                                     {
                                                         OldPassword = oldPassword,
                                                         NewPassword = newPassword
                                                     }),
                                                 new MediaTypeHeaderValue("application/json")));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }
}