using Blazored.LocalStorage;
using Restaurant.Models.Classes;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Restaurant.Services
{
    public class OwnerService
    {
        private readonly HttpClient _httpClient;
		private readonly ILocalStorageService _localStorage;

		public OwnerService(HttpClient httpClient , ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
			_localStorage = localStorage;
        }

        // Get all owners
        public async Task<List<Owner>> GetOwnersAsync()
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			return await _httpClient.GetFromJsonAsync<List<Owner>>("api/Owners");
        }

        // Get a single owner by ID
        public async Task<Owner> GetOwnerByIdAsync(int id)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			return await _httpClient.GetFromJsonAsync<Owner>($"api/Owners/{id}");
        }

        // Create a new owner
        public async Task<bool> CreateOwnerAsync(Owner owner)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PostAsJsonAsync("api/Owners", owner);
            return response.IsSuccessStatusCode;
        }

        // Update an existing owner
        public async Task<bool> UpdateOwnerAsync(int id, Owner owner)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PutAsJsonAsync($"api/Owners/{id}", owner);
            return response.IsSuccessStatusCode;
        }

        // Delete an owner
        public async Task<bool> DeleteOwnerAsync(int id)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.DeleteAsync($"api/Owners/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
