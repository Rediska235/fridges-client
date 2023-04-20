using Fridges.Client.Models.DTOs;
using Fridges.Client.Models.Entities;
using Fridges.Client.Services.Interfaces;
using Microsoft.Net.Http.Headers;
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

        user.Username = user.Username.Trim();
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
            return String.Empty;
        }

        string errorMessage = await response.Content.ReadAsStringAsync();
        errorMessage = errorMessage.Substring(10, errorMessage.Length - 12);

        return errorMessage;
    }

    public async Task<string> Login(UserDto user)
    {
        var uri = new Uri(_httpClient.BaseAddress + "login");

        user.Username = user.Username.Trim();
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
        errorMessage = errorMessage.Substring(10, errorMessage.Length - 12);

        return errorMessage;
    }

    public void Logout()
    {
        _httpContextAccessor.HttpContext.Session.SetString("jwtToken", "");
    }

    public async Task RefreshToken()
    {
        string jwtToken = "";

        bool tokenExist = _httpClient.DefaultRequestHeaders.TryGetValues(HeaderNames.Authorization, out var headerTokens);
        if (!tokenExist)
        {
            jwtToken = _httpContextAccessor.HttpContext.Session.GetString("jwtToken");
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + jwtToken);
        }

        var response = _httpClient.GetAsync("refresh-token").Result;
        if (response.IsSuccessStatusCode)
        {
            jwtToken = await response.Content.ReadAsStringAsync();
            jwtToken = jwtToken.Replace("\"", "");

            _httpContextAccessor.HttpContext.Session.SetString("jwtToken", jwtToken);
        }
    }

    public async Task GiveRoles(GiveRoleDto giveRoleDto)
    {
        var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("jwtToken");
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + jwtToken);

        var uri = new Uri(_httpClient.BaseAddress + "give-role");

        var json = JsonSerializer.Serialize(giveRoleDto);

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = uri,
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var response = await _httpClient.SendAsync(request);
    }

    public async Task<IEnumerable<UserOutputDto>> GetAllUsers()
    {
        bool tokenExist = _httpClient.DefaultRequestHeaders.TryGetValues(HeaderNames.Authorization, out var headerTokens);
        if (!tokenExist)
        {
            var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("jwtToken");
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + jwtToken);
        }

        IEnumerable<UserOutputDto> users = new List<UserOutputDto>();

        var response = await _httpClient.GetAsync("users");
        if (response.IsSuccessStatusCode)
        {
            users = response.Content.ReadFromJsonAsync<IEnumerable<UserOutputDto>>().Result;
        }

        return users;
    }

    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        bool tokenExist = _httpClient.DefaultRequestHeaders.TryGetValues(HeaderNames.Authorization, out var headerTokens);
        if (!tokenExist)
        {
            var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("jwtToken");
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + jwtToken);
        }

        IEnumerable<Role> users = new List<Role>();

        var response = await _httpClient.GetAsync("roles");
        if (response.IsSuccessStatusCode)
        {
            users = response.Content.ReadFromJsonAsync<IEnumerable<Role>>().Result;
        }

        return users;
    }
}
