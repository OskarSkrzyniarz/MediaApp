using System.Net.Http.Json;

namespace SocialMediaApp.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;
        public AuthenticationService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<bool> Login(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5003/api/auth/login", new { username, password });
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Register(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5003/api/auth/register", new { username, password });
            return response.IsSuccessStatusCode;
        }
    }
}
