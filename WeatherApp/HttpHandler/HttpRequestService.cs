using System.Text.Json;

namespace HttpHandler;

public class HttpRequestService
{
    private readonly HttpClient _httpClient;
    public HttpRequestService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> SendAsync<T>(string url, string parameter, HttpMethod methodType)
    {
        var uri = new Uri(url + parameter);
        var request = new HttpRequestMessage
        {
            RequestUri = uri ,
            Method = methodType
        };
        
        var result = await _httpClient.SendAsync(request);
        if (!result.IsSuccessStatusCode) return default;
        var response = await result.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(response);
    }
}