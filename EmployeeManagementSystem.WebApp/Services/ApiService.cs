using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.WebApp.Services;

public class ApiService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _clientFactory = clientFactory;
        _httpContextAccessor = httpContextAccessor;
    }
    
    private HttpClient CreateClient()
    {
        var client = _clientFactory.CreateClient();
        var token = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return client;
    }
    
    public async Task<T> GetAsync<T>(string url)
    {
        using var client = CreateClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }
    
    public async Task<T> PostAsync<T>(string url, object data)
    {
        using var client = CreateClient();
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseContent);
    }
}