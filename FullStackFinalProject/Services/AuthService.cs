using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FullStackFinalProject.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        public bool IsLoggedIn { get; private set; }
        public string? Username { get; private set; }
        public string? Role { get; private set; }

        public event Action? OnAuthStateChanged;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> Login(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/login", new { Email = email, Password = password });
            if (response.IsSuccessStatusCode)
            {
                var userInfo = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (userInfo != null)
                {
                    Username = userInfo.Username;
                    Role = userInfo.Role;
                    IsLoggedIn = true;
                    NotifyAuthStateChanged();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> Register(string username, string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/register", new { Username = username, Email = email, PasswordHash = password });
            return response.IsSuccessStatusCode;
        }

        public void Logout()
        {
            Username = null;
            Role = null;
            IsLoggedIn = false;
            NotifyAuthStateChanged();
        }

        private void NotifyAuthStateChanged() => OnAuthStateChanged?.Invoke();

        public record LoginResponse(string Username, string Role);
    }
}
