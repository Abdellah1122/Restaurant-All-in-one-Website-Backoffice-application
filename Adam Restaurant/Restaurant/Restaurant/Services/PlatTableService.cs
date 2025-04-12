using Restaurant.Models.Classes;
using System.Net.Http.Json;

namespace Restaurant.Services
{
    public class PlatTableService
    {
        private readonly HttpClient _httpClient;

        public PlatTableService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all PlatTables
        public async Task<List<PlatTable>> GetPlatTablesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PlatTable>>("api/PlatTables");
        }

        // Get a single PlatTable by ID
        public async Task<PlatTable> GetPlatTableByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PlatTable>($"api/PlatTables/{id}");
        }

        // Create a new PlatTable
        public async Task<bool> CreatePlatTableAsync(PlatTable platTable)
        {
            var response = await _httpClient.PostAsJsonAsync("api/PlatTables", platTable);
            return response.IsSuccessStatusCode;
        }

        // Update an existing PlatTable
        public async Task<bool> UpdatePlatTableAsync(int id, PlatTable platTable)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/PlatTables/{id}", platTable);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> IncrementQuantityAsync(int id)
        {
            var response = await _httpClient.PutAsync($"api/PlatTables/incrementQuantity/{id}", null);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> ADDQuantityAsync(int id)
        {
            var response = await _httpClient.PutAsync($"api/PlatTables/ADDQuantity/{id}", null);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> MINQuantityAsync(int id)
        {
            var response = await _httpClient.PutAsync($"api/PlatTables/MINQuantity/{id}", null);
            return response.IsSuccessStatusCode;
        }



        // Delete a PlatTable
        public async Task<bool> DeletePlatTableAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/PlatTables/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
