using DesktopClient.Entity;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DesktopClient.RequestingServices;
internal class OperationCategoriesRequestingService
{
    private static readonly string ServerUrl;

    private static readonly HttpClient HttpClient;

    static OperationCategoriesRequestingService()
    {
        ServerUrl = "https://localhost:7093";
        HttpClient = new HttpClient();
        HttpClient.BaseAddress = new Uri($"{ServerUrl}/OperationCategory");
        HttpClient.Timeout = TimeSpan.FromSeconds(3);
    }

    public async Task<ICollection<OperationCategory>> GetByCurrentDepartmentAsync()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync($"/OperationCategory/GetByDepartmentId/{JwtTokenVault.DepartmentId}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<ICollection<OperationCategory>>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<ICollection<OperationCategory>> GetAllAsync()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync("/OperationCategory/GetAll");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<ICollection<OperationCategory>>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<OperationCategory> GetByIdAsync(long id)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync($"/OperationCategory/GetById/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<OperationCategory>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<ICollection<OperationCategory>> GetByDepartmentIdAsync(long id)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync($"/OperationCategory/GetByDepartmentId/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<ICollection<OperationCategory>>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }
}
