using System.Net.Http.Json;

namespace Restaurant.Services
{
    public class JwtTokenService
    {
        private readonly HttpClient _httpClient;

        public JwtTokenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", new
            {
                Username = username,
                Password = password
            });

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return content?.Token;
            }

            return null;
        }
    }

    public class TokenResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}
