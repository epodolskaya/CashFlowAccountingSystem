using DesktopClient.Entity;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DesktopClient.RequestingServices;

internal class PositionsRequestingService
{
    private static readonly string ServerUrl;

    private static readonly HttpClient HttpClient;

    static PositionsRequestingService()
    {
        ServerUrl = ConfigurationManager.AppSettings.Get("serverUrl")!;
        HttpClient = new HttpClient();
        HttpClient.BaseAddress = new Uri($"{ServerUrl}");
        HttpClient.Timeout = TimeSpan.FromSeconds(5);
    }

    public async Task<ICollection<Position>> GetAllAsync()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync("/Position/GetAll");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<ICollection<Position>>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<Position> GetByIdAsync(long id)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync($"/Position/GetById/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<Position>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<ICollection<Position>> GetByDepartmentIdAsync(long id)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync($"/Position/GetByDepartmentId/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<ICollection<Position>>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<Position> CreateAsync(Position position)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);

        HttpResponseMessage response = await HttpClient.PostAsync
                                           ("/Position/Create",
                                            new StringContent
                                                (JsonSerializer.Serialize
                                                     (position,
                                                      new JsonSerializerOptions(JsonSerializerDefaults.Web)),
                                                 new MediaTypeHeaderValue("application/json")));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<Position>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<Position> UpdateAsync(Position position)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);

        HttpResponseMessage response = await HttpClient.PutAsync
                                           ("/Position/Update",
                                            new StringContent
                                                (JsonSerializer.Serialize(position),
                                                 new MediaTypeHeaderValue("application/json")));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<Position>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task DeleteAsync(long id)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.DeleteAsync($"/Position/Delete/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }
}