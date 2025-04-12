using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Restaurant.Models.Classes;
using Restaurant.Models.Enums;

namespace Restaurant.Services
{
    public class TableService
    {
        private readonly HttpClient _httpClient;
		private readonly ILocalStorageService _localStorage;

		public TableService(HttpClient httpClient , ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
			_localStorage = localStorage;
		}

        // Get all tables
        public async Task<List<Table>> GetTablesAsync()
        {
			return await _httpClient.GetFromJsonAsync<List<Table>>("api/Tables");
        }

        // Get a single table by ID
        public async Task<Table> GetTableByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Table>($"api/Tables/{id}");
        }

        // Create a new table
        public async Task<bool> CreateTableAsync(Table table)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PostAsJsonAsync("api/Tables", table);
            return response.IsSuccessStatusCode;
        }

        // Update an existing table
        public async Task<bool> UpdateTableAsync(int id, Table table)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PutAsJsonAsync($"api/Tables/{id}", table);
            return response.IsSuccessStatusCode;
        }

        // Delete a table
        public async Task<bool> DeleteTableAsync(int id)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.DeleteAsync($"api/Tables/{id}");
            return response.IsSuccessStatusCode;
        }

        // Update table status
        public async Task<bool> UpdateTableStatusAsync(int id, StatutTable status)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PutAsync($"api/Tables/{id}/Status/{status}", null);
            return response.IsSuccessStatusCode;
        }
		// Increment NbrFoisReserve
		public async Task<bool> IncrementNbrFoisReserveAsync(int id)
		{
			
			var response = await _httpClient.PutAsync($"api/Tables/IncrementNbrFoisReserve?id={id}", null);
			return response.IsSuccessStatusCode;
		}

		// Increment NbrFoisOccupe
		public async Task<bool> IncrementNbrFoisOccupeAsync(int id)
		{
			
			var response = await _httpClient.PutAsync($"api/Tables/IncrementNbrFoisOccupe?id={id}", null);
			return response.IsSuccessStatusCode;
		}

	}

}
