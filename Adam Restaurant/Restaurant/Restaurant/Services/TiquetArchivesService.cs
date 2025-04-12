using Blazored.LocalStorage;
using Restaurant.Models.Classes;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Restaurant.Services
{
	public class TiquetArchiveService
	{
		private readonly HttpClient _httpClient;
		private readonly ILocalStorageService _localStorage;

		public TiquetArchiveService(HttpClient httpClient, ILocalStorageService localStorage)
		{
			_httpClient = httpClient;
			_localStorage = localStorage;
		}

		private async Task SetAuthorizationHeaderAsync()
		{
			var token = await _localStorage.GetItemAsync<string>("authToken");
			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
		}

		// Get all archived tickets
		public async Task<List<TiquetArchive>?> GetTiquetArchivesAsync()
		{
			await SetAuthorizationHeaderAsync();
			return await _httpClient.GetFromJsonAsync<List<TiquetArchive>>("api/TiquetArchives");
		}

		// Get a single archived ticket by ID
		public async Task<TiquetArchive?> GetTiquetArchiveAsync(int id)
		{
			await SetAuthorizationHeaderAsync();
			return await _httpClient.GetFromJsonAsync<TiquetArchive>($"api/TiquetArchives/{id}");
		}

		// Create a new archived ticket
		public async Task<TiquetArchive?> CreateTiquetArchiveAsync(TiquetArchive tiquetArchive)
		{
			await SetAuthorizationHeaderAsync();
			var response = await _httpClient.PostAsJsonAsync("api/TiquetArchives", tiquetArchive);
			return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<TiquetArchive>() : null;
		}

		// Delete an archived ticket
		public async Task<bool> DeleteTiquetArchiveAsync(int id)
		{
			await SetAuthorizationHeaderAsync();
			var response = await _httpClient.DeleteAsync($"api/TiquetArchives/{id}");
			return response.IsSuccessStatusCode;
		}
	}
}
