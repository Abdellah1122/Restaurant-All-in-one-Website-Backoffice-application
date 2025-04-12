using Blazored.LocalStorage;
using Restaurant.Models.Classes;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Restaurant.Services
{
    public class CategoriePlatService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/CategoriePlats";
		private readonly ILocalStorageService _localStorage;

		public CategoriePlatService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
			_localStorage = localStorage;
		}

        public async Task<List<CategoriePlat>> GetCategoriePlatsAsync()
        {
			
			return await _httpClient.GetFromJsonAsync<List<CategoriePlat>>(BaseUrl);
        }

        public async Task<CategoriePlat> GetCategoriePlatByIdAsync(int id)
        {
			
			return await _httpClient.GetFromJsonAsync<CategoriePlat>($"{BaseUrl}/{id}");
        }

        public async Task<bool> CreateCategoriePlatAsync(CategoriePlat categoriePlat)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PostAsJsonAsync(BaseUrl, categoriePlat);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCategoriePlatAsync(int id, CategoriePlat categoriePlat)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", categoriePlat);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCategoriePlatAsync(int id)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
