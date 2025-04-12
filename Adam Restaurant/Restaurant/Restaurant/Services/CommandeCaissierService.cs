using Restaurant.Models.Classes;  // Adjust the namespace according to your project
using System.Net.Http.Json;

namespace Restaurant.Services
{
    public class CommandeCaissierService
    {
        private readonly HttpClient _httpClient;

        public CommandeCaissierService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all CommandeCaissiers
        public async Task<List<CommandeCaissier>> GetCommandeCaissiersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CommandeCaissier>>("api/CommandeCaissiers");
        }

        // Get a single CommandeCaissier by ID
        public async Task<CommandeCaissier> GetCommandeCaissierAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CommandeCaissier>($"api/CommandeCaissiers/{id}");
        }

        // Create a new CommandeCaissier
        public async Task<CommandeCaissier> CreateCommandeCaissierAsync(CommandeCaissier commandeCaissier)
        {
            var response = await _httpClient.PostAsJsonAsync("api/CommandeCaissiers", commandeCaissier);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CommandeCaissier>();
        }

        // Update an existing CommandeCaissier
        public async Task UpdateCommandeCaissierAsync(int id, CommandeCaissier commandeCaissier)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/CommandeCaissiers/{id}", commandeCaissier);
            response.EnsureSuccessStatusCode();
        }

        // Delete a CommandeCaissier by ID
        public async Task DeleteCommandeCaissierAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/CommandeCaissiers/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
