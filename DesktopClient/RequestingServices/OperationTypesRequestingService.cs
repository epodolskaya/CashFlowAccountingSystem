using DesktopClient.Entity;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DesktopClient.RequestingServices;

internal class OperationTypesRequestingService
{
    private static readonly string ServerUrl;

    private static readonly HttpClient HttpClient;

    static OperationTypesRequestingService()
    {
        ServerUrl = "https://localhost:7093";
        HttpClient = new HttpClient();
        HttpClient.BaseAddress = new Uri($"{ServerUrl}");
        HttpClient.Timeout = TimeSpan.FromSeconds(5);
    }

    public async Task<ICollection<OperationType>> GetAllAsync()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync("/OperationType/GetAll");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<ICollection<OperationType>>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<OperationType> GetByIdAsync(long id)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync($"/OperationType/GetById/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<OperationType>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<ICollection<OperationType>> GetByDepartmentIdAsync(long id)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync($"/OperationType/GetByDepartmentId/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<ICollection<OperationType>>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }
}