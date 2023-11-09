using DesktopClient.Commands.Abstractions;
using DesktopClient.Entity.BaseEntity;
using DesktopClient.RequestingService.Abstractions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DesktopClient.RequestingService;

internal class RequestingService<T> : IRequestingService<T> where T : StorableEntity
{
    private static readonly string ServerUrl;

    private static readonly HttpClient HttpClient;

    private static readonly string TypeName = typeof(T).Name;

    static RequestingService()
    {
        ServerUrl = "https://localhost:7093";
        HttpClient = new HttpClient();
        HttpClient.BaseAddress = new Uri($"{ServerUrl}/{TypeName}");
        HttpClient.Timeout = TimeSpan.FromSeconds(3);
    }

    public async Task<ICollection<T>> GetAllAsync()
    {
        HttpResponseMessage response = await HttpClient.GetAsync("");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<ICollection<T>>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<T> GetByIdAsync(long id)
    {
        HttpResponseMessage response = await HttpClient.GetAsync("/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<T>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<T> CreateAsync(CreateCommand<T> createCommand)
    {
        HttpResponseMessage response = await HttpClient.PostAsync
                                           (string.Empty, new StringContent
                                               (JsonSerializer.Serialize
                                                    (createCommand),
                                                new MediaTypeHeaderValue("application/json")));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<T>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task<T> UpdateAsync(UpdateCommand<T> updateCommand)
    {
        HttpResponseMessage response = await HttpClient.PutAsync
                                           (string.Empty, new StringContent
                                               (JsonSerializer.Serialize
                                                    (updateCommand),
                                                new MediaTypeHeaderValue("application/json")));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        return JsonSerializer.Deserialize<T>
            (await response.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public async Task DeleteAsync(long id)
    {
        HttpResponseMessage response = await HttpClient.DeleteAsync("/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }
}