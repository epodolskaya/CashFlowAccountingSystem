using DesktopClient.Commands.Abstractions;
using DesktopClient.Entity.BaseEntity;
using DesktopClient.RequestingService.Abstractions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DesktopClient.RequestingService;

internal class RequestingService<TEntity> : IRequestingService<TEntity> where TEntity : StorableEntity
{
    private static readonly string ServerUrl;

    private static readonly HttpClient HttpClient;

    private static readonly string TypeName = typeof(TEntity).Name;

    static RequestingService()
    {
        ServerUrl = "https://localhost:7093";
        HttpClient = new HttpClient();
        HttpClient.BaseAddress = new Uri($"{ServerUrl}/{TypeName}");
        HttpClient.Timeout = TimeSpan.FromSeconds(3);
    }

    public async Task<ICollection<TEntity>> GetAllAsync()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync("");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<ICollection<TEntity>>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<TEntity> GetByIdAsync(long id)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.GetAsync($"/{TypeName}/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<TEntity>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<TEntity> CreateAsync<TCommand>(CreateCommand<TEntity> createCommand)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);

        HttpResponseMessage response = await HttpClient.PostAsync
                                           (string.Empty,
                                            new StringContent
                                                (JsonSerializer.Serialize
                                                     ((TCommand)(object)createCommand,
                                                      new JsonSerializerOptions(JsonSerializerDefaults.Web)),
                                                 new MediaTypeHeaderValue("application/json")));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<TEntity>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<TEntity> UpdateAsync<TCommand>(UpdateCommand<TEntity> updateCommand)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);

        HttpResponseMessage response = await HttpClient.PutAsync
                                           (string.Empty,
                                            new StringContent
                                                (JsonSerializer.Serialize((TCommand)(object)updateCommand),
                                                 new MediaTypeHeaderValue("application/json")));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<TEntity>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task DeleteAsync(long id)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenVault.JwtTokenString);
        HttpResponseMessage response = await HttpClient.DeleteAsync($"/{TypeName}/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }
}