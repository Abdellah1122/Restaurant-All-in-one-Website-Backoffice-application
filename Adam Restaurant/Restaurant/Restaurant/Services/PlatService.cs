using Blazored.LocalStorage;
using Restaurant.Models.Classes;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Restaurant.Services
{
    public class PlatService
    {
        private readonly HttpClient _httpClient;
		private readonly ILocalStorageService _localStorage;

		public PlatService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        // Get all plats
        public async Task<List<Plat>> GetPlatsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Plat>>("api/Plats");
        }

        // Get a single plat by ID
        public async Task<Plat> GetPlatByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Plat>($"api/Plats/{id}");
        }

        // Create a new plat
        public async Task<bool> CreatePlatAsync(Plat plat)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PostAsJsonAsync("api/Plats", plat);
            return response.IsSuccessStatusCode;
        }

        // Update an existing plat
        public async Task<bool> UpdatePlatAsync(int id, Plat plat)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PutAsJsonAsync($"api/Plats/{id}", plat);
            return response.IsSuccessStatusCode;
        }
		public async Task<bool> ChangePrixAsync(int id, double prix)
		{
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PutAsJsonAsync($"api/plat/ChangePrix/{id}", prix);
			return response.IsSuccessStatusCode;
		}

		// Delete a plat
		public async Task<bool> DeletePlatAsync(int id)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.DeleteAsync($"api/Plats/{id}");
            return response.IsSuccessStatusCode;
        }
		public async Task<bool> IncrementNbrFoisCommandeAsync(int id)
		{
			var response = await _httpClient.PutAsync($"api/Plats/IncrementNbrFoisCommande/{id}", null);
			return response.IsSuccessStatusCode;
		}
	}
}
