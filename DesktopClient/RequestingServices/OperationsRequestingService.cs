using DesktopClient.Entity;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DesktopClient.RequestingServices;

internal class OperationsRequestingService
{
    private static readonly string ServerUrl;

    private static readonly HttpClient HttpClient;

    static OperationsRequestingService()
    {
        ServerUrl = ConfigurationManager.AppSettings.Get("serverUrl")!;
        HttpClient = new HttpClient();
        HttpClient.BaseAddress = new Uri($"{ServerUrl}");
        HttpClient.Timeout = TimeSpan.FromSeconds(5);
    }

    public async Task<ICollection<Operation>> GetByCurrentDepartmentAsync()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync($"/Operation/GetByDepartmentId/{JwtTokenVault.DepartmentId}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<ICollection<Operation>>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<ICollection<Operation>> GetAllAsync()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync("/Operation/GetAll");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<ICollection<Operation>>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<Operation> GetByIdAsync(long id)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync($"/Operation/GetById/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<Operation>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<ICollection<Operation>> GetByDepartmentIdAsync(long id)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync($"/Operation/GetByDepartmentId/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<ICollection<Operation>>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<Operation> CreateAsync(Operation operation)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);

        HttpResponseMessage response = await HttpClient.PostAsync
                                           ("/Operation/Create",
                                            new StringContent
                                                (JsonSerializer.Serialize
                                                     (operation,
                                                      new JsonSerializerOptions(JsonSerializerDefaults.Web)),
                                                 new MediaTypeHeaderValue("application/json")));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<Operation>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<Operation> UpdateAsync(Operation operation)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);

        HttpResponseMessage response = await HttpClient.PutAsync
                                           ("/Operation/Update",
                                            new StringContent
                                                (JsonSerializer.Serialize(operation),
                                                 new MediaTypeHeaderValue("application/json")));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<Operation>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task DeleteAsync(long id)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.DeleteAsync($"/Operation/Delete/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }
}