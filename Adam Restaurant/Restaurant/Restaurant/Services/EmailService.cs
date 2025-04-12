using Restaurant.Models;
using System.Net.Http.Json;

namespace Restaurant.Services
{
    public class EmailService
    {
        private readonly HttpClient _httpClient;


        public EmailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> SendEmailAsync(EmailDTO emailDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Email/send", emailDto);
            return response.IsSuccessStatusCode;
        }
    }
}
