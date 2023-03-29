using Fridges.Client.Models.DTOs;
using Fridges.Client.Services.Interfaces;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Fridges.Client.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private HttpClient _httpClient;

    public AuthService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;

        _httpClient = _httpClientFactory.CreateClient("Auth");
    }

    public async Task<string> Register(UserDto user)
    {
        var uri = new Uri(_httpClient.BaseAddress + "register");

        var json = JsonSerializer.Serialize(user);

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = uri,
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var response = await _httpClient.SendAsync(request);
        if(response.IsSuccessStatusCode)
        {
            return String.Empty;
        }

        string errorMessage = await response.Content.ReadAsStringAsync();
        errorMessage = errorMessage.Replace("\"", "");

        return errorMessage;
    }

    public async Task<string> Login(UserDto user)
    {
        var uri = new Uri(_httpClient.BaseAddress + "login");

        var json = JsonSerializer.Serialize(user);

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = uri,
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var jwtToken = await response.Content.ReadAsStringAsync();
            jwtToken = jwtToken.Replace("\"", "");

            _httpContextAccessor.HttpContext.Session.SetString("jwtToken", jwtToken);
            return String.Empty;
        }

        string errorMessage = await response.Content.ReadAsStringAsync();
        errorMessage = errorMessage.Replace("\"", "");

        return errorMessage;
    }

    public void Logout()
    {
        _httpContextAccessor.HttpContext.Session.SetString("jwtToken", "");
    }
}
