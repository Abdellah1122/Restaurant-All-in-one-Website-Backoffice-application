using Blazored.LocalStorage;
using Restaurant.Models.Classes;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Restaurant.Services
{
	public class CategorieFournisseurService
	{
		private readonly HttpClient _httpClient;
		private readonly ILocalStorageService _localStorage;
		public CategorieFournisseurService(HttpClient httpClient , ILocalStorageService localStorage)
		{
			_httpClient = httpClient;
			_localStorage= localStorage;
		}

		// Get all CategorieFournisseurs
		public async Task<List<CategorieFournisseur>> GetCategorieFournisseursAsync()
		{
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			return await _httpClient.GetFromJsonAsync< List<CategorieFournisseur>>("api/CategorieFournisseurs");
			
		}

		// Get a specific CategorieFournisseur by ID
		public async Task<CategorieFournisseur> GetCategorieFournisseurByIdAsync(int id)
		{
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.GetFromJsonAsync<CategorieFournisseur>($"api/CategorieFournisseurs/{id}");
			return response;
		}

		// Create a new CategorieFournisseur
		public async Task CreateCategorieFournisseurAsync(CategorieFournisseur categorieFournisseur)
		{
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PostAsJsonAsync("api/CategorieFournisseurs", categorieFournisseur);
			response.EnsureSuccessStatusCode();
		}

		// Update an existing CategorieFournisseur
		public async Task UpdateCategorieFournisseurAsync(int id, CategorieFournisseur categorieFournisseur)
		{
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PutAsJsonAsync($"api/CategorieFournisseurs/{id}", categorieFournisseur);
			response.EnsureSuccessStatusCode();
		}

		// Delete a CategorieFournisseur by ID
		public async Task DeleteCategorieFournisseurAsync(int id)
		{
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.DeleteAsync($"api/CategorieFournisseurs/{id}");
			response.EnsureSuccessStatusCode();
		}
	}

}
